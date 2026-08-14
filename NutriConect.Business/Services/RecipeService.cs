using NutriConect.Business.Entities;
using NutriConect.Business.Enums;
using NutriConect.Business.InputModels;
using NutriConect.Business.Interfaces;
using NutriConect.Business.Interfaces.Repositories;
using NutriConect.Business.Interfaces.Services;
using NutriConect.Business.ViewModels;

namespace NutriConect.Business.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IRecipeReviewRepository _reviewRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RecipeService(
        IRecipeRepository recipeRepository,
        IRecipeReviewRepository reviewRepository,
        IRepository<User> userRepository,
        IUnitOfWork unitOfWork)
    {
        _recipeRepository = recipeRepository;
        _reviewRepository = reviewRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RecipeListViewModel> ListAsync(RecipeCategoryEnum? category, string? query, RecipeSortEnum sort)
    {
        var recipes = (await _recipeRepository.GetAllWithAuthorAsync(category, query)).ToList();

        if (sort == RecipeSortEnum.MelhorAvaliadas && recipes.Count > 0)
        {
            var summaries = await _reviewRepository.GetSummariesAsync(recipes.Select(r => r.Id));
            recipes = recipes
                .OrderByDescending(r => summaries.TryGetValue(r.Id, out var s) ? s.Average : 0)
                .ThenByDescending(r => summaries.TryGetValue(r.Id, out var s) ? s.Count : 0)
                .ThenByDescending(r => r.CreatedAt)
                .ToList();
        }

        return new RecipeListViewModel
        {
            Category = category,
            Query = query,
            Sort = sort,
            Recipes = recipes.Select(r => new RecipeCardViewModel
            {
                Id = r.Id,
                Title = r.Title,
                PhotoUrl = r.PhotoUrl,
                PrepMinutes = r.PrepMinutes,
                CaloriesPerServing = r.CaloriesPerServing,
                Categories = ParseEnumList<RecipeCategoryEnum>(r.Categories),
                ApprovedByNutritionist = r.ApprovedByNutritionist,
                AuthorName = AuthorDisplayName(r.Author)
            }).ToList()
        };
    }

    public async Task<RecipeDetailViewModel?> GetByIdAsync(int id, int? currentUserId)
    {
        var recipe = await _recipeRepository.GetWithDetailsAsync(id);
        if (recipe is null) return null;

        var reviews = await _reviewRepository.GetByRecipeAsync(id);
        var count = reviews.Count;
        var isAuthor = currentUserId.HasValue && recipe.AuthorId == currentUserId.Value;
        var alreadyReviewed = currentUserId.HasValue && reviews.Any(r => r.AuthorId == currentUserId.Value);

        return new RecipeDetailViewModel
        {
            Id = recipe.Id,
            Title = recipe.Title,
            ShortDescription = recipe.ShortDescription,
            PhotoUrl = recipe.PhotoUrl,
            PrepMinutes = recipe.PrepMinutes,
            Servings = recipe.Servings,
            Difficulty = recipe.Difficulty,
            CaloriesPerServing = recipe.CaloriesPerServing,
            Categories = ParseEnumList<RecipeCategoryEnum>(recipe.Categories),
            ApprovedByNutritionist = recipe.ApprovedByNutritionist,
            AuthorName = AuthorDisplayName(recipe.Author),
            Ingredients = recipe.Ingredients.OrderBy(i => i.Order).Select(i => i.Text).ToList(),
            Steps = recipe.Steps.OrderBy(s => s.Order).Select(s => s.Text).ToList(),
            CanEdit = isAuthor,

            AverageRating = count > 0 ? Math.Round(reviews.Average(r => r.Rating), 1) : null,
            ReviewCount = count,
            Distribution = BuildDistribution(reviews),
            Reviews = reviews.Select(r => MapToReviewViewModel(r, currentUserId)).ToList(),
            AlreadyReviewed = alreadyReviewed,
            CanReview = currentUserId.HasValue && !isAuthor && !alreadyReviewed
        };
    }

    private static List<RatingBucketViewModel> BuildDistribution(List<RecipeReview> reviews)
    {
        var total = reviews.Count;
        var buckets = new List<RatingBucketViewModel>();
        for (var star = 5; star >= 1; star--)
        {
            var c = reviews.Count(r => r.Rating == star);
            buckets.Add(new RatingBucketViewModel
            {
                Star = star,
                Count = c,
                Percent = total > 0 ? (int)Math.Round(c * 100.0 / total) : 0
            });
        }
        return buckets;
    }

    private static RecipeReviewViewModel MapToReviewViewModel(RecipeReview r, int? currentUserId)
    {
        var name = r.IsAnonymous ? "Anônimo" : AuthorDisplayName(r.Author);
        return new RecipeReviewViewModel
        {
            Id = r.Id,
            Rating = r.Rating,
            Comment = r.Comment,
            IsAnonymous = r.IsAnonymous,
            AuthorName = string.IsNullOrWhiteSpace(name) ? "Anônimo" : name,
            AuthorInitials = Initials(r.IsAnonymous ? "Anônimo" : AuthorDisplayName(r.Author)),
            CreatedAt = r.CreatedAt,
            CanDelete = currentUserId.HasValue && r.AuthorId == currentUserId.Value
        };
    }

    private static string Initials(string name) =>
        string.IsNullOrWhiteSpace(name)
            ? "?"
            : string.Concat(name.Split(' ', StringSplitOptions.RemoveEmptyEntries).Take(2).Select(w => char.ToUpper(w[0])));

    public async Task<RecipeInputModel?> GetForEditAsync(int id, int authorId)
    {
        var recipe = await _recipeRepository.GetWithDetailsAsync(id);
        if (recipe is null || recipe.AuthorId != authorId) return null;

        return new RecipeInputModel
        {
            Title = recipe.Title,
            ShortDescription = recipe.ShortDescription,
            PrepMinutes = recipe.PrepMinutes,
            Servings = recipe.Servings,
            Difficulty = recipe.Difficulty,
            CaloriesPerServing = recipe.CaloriesPerServing,
            Categories = ParseEnumList<RecipeCategoryEnum>(recipe.Categories),
            Ingredients = recipe.Ingredients.OrderBy(i => i.Order).Select(i => i.Text).ToList(),
            Steps = recipe.Steps.OrderBy(s => s.Order).Select(s => s.Text).ToList()
        };
    }

    public async Task<int> CreateAsync(int authorId, RecipeInputModel input)
    {
        var author = await _userRepository.GetByIdAsync(authorId);

        var recipe = new Recipe
        {
            Title = input.Title.Trim(),
            ShortDescription = string.IsNullOrWhiteSpace(input.ShortDescription) ? null : input.ShortDescription.Trim(),
            PrepMinutes = input.PrepMinutes,
            Servings = input.Servings,
            Difficulty = input.Difficulty,
            CaloriesPerServing = input.CaloriesPerServing,
            Categories = JoinCategories(input.Categories),
            AuthorId = authorId,
            ApprovedByNutritionist = author is Nutritionist
        };

        ApplyChildren(recipe, input);

        await _recipeRepository.AddAsync(recipe);
        await _unitOfWork.SaveChangesAsync();
        return recipe.Id;
    }

    public async Task<bool> UpdateAsync(int id, int authorId, RecipeInputModel input)
    {
        var recipe = await _recipeRepository.GetWithDetailsAsync(id);
        if (recipe is null || recipe.AuthorId != authorId) return false;

        recipe.Title = input.Title.Trim();
        recipe.ShortDescription = string.IsNullOrWhiteSpace(input.ShortDescription) ? null : input.ShortDescription.Trim();
        recipe.PrepMinutes = input.PrepMinutes;
        recipe.Servings = input.Servings;
        recipe.Difficulty = input.Difficulty;
        recipe.CaloriesPerServing = input.CaloriesPerServing;
        recipe.Categories = JoinCategories(input.Categories);

        recipe.Ingredients.Clear();
        recipe.Steps.Clear();
        ApplyChildren(recipe, input);

        _recipeRepository.Update(recipe);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id, int authorId)
    {
        var recipe = await _recipeRepository.GetWithDetailsAsync(id);
        if (recipe is null || recipe.AuthorId != authorId) return false;

        _recipeRepository.Remove(recipe);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    private static void ApplyChildren(Recipe recipe, RecipeInputModel input)
    {
        var order = 0;
        foreach (var text in input.Ingredients)
        {
            if (string.IsNullOrWhiteSpace(text)) continue;
            recipe.Ingredients.Add(new RecipeIngredient { Text = text.Trim(), Order = order++ });
        }

        order = 0;
        foreach (var text in input.Steps)
        {
            if (string.IsNullOrWhiteSpace(text)) continue;
            recipe.Steps.Add(new RecipeStep { Text = text.Trim(), Order = order++ });
        }
    }

    private static string AuthorDisplayName(User user) =>
        user is Nutritionist n && !string.IsNullOrWhiteSpace(n.DisplayName) ? n.DisplayName : user.FullName;

    private static string? JoinCategories(List<RecipeCategoryEnum> categories) =>
        categories.Count > 0 ? string.Join(',', categories) : null;

    private static List<T> ParseEnumList<T>(string? value) where T : struct, Enum =>
        string.IsNullOrWhiteSpace(value)
            ? []
            : [.. value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                       .Select(s => Enum.TryParse<T>(s, out var v) ? (T?)v : null)
                       .OfType<T>()];
}

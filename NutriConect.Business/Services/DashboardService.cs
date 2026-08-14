using NutriConect.Business.Entities;
using NutriConect.Business.Enums;
using NutriConect.Business.Interfaces.Repositories;
using NutriConect.Business.Interfaces.Services;
using NutriConect.Business.ViewModels;

namespace NutriConect.Business.Services;

public class DashboardService : IDashboardService
{
    private const int ListSize = 3;

    private readonly IClientService _clientService;
    private readonly INutritionistService _nutritionistService;
    private readonly IRecipeService _recipeService;
    private readonly ITipService _tipService;
    private readonly IChatService _chatService;
    private readonly IRecipeRepository _recipeRepository;
    private readonly ITipRepository _tipRepository;
    private readonly INutritionistReviewRepository _reviewRepository;

    public DashboardService(
        IClientService clientService,
        INutritionistService nutritionistService,
        IRecipeService recipeService,
        ITipService tipService,
        IChatService chatService,
        IRecipeRepository recipeRepository,
        ITipRepository tipRepository,
        INutritionistReviewRepository reviewRepository)
    {
        _clientService = clientService;
        _nutritionistService = nutritionistService;
        _recipeService = recipeService;
        _tipService = tipService;
        _chatService = chatService;
        _recipeRepository = recipeRepository;
        _tipRepository = tipRepository;
        _reviewRepository = reviewRepository;
    }

    public async Task<ClientDashboardViewModel?> GetClientDashboardAsync(int userId)
    {
        var profile = await _clientService.GetProfileAsync(userId);
        if (profile is null) return null;

        var conversations = await _chatService.GetConversationsAsync(userId);
        var recipes = await _recipeService.ListAsync(null, null, RecipeSortEnum.MelhorAvaliadas);
        var tips = await _tipService.ListAsync(null, onlyProfessionals: true, userId);

        return new ClientDashboardViewModel
        {
            FirstName = FirstName(profile.FullName),
            Initials = profile.Initials,
            MainGoal = profile.MainGoal,
            UnreadMessages = await _chatService.GetUnreadCountAsync(userId),
            RecentConversations = conversations.Take(ListSize).ToList(),
            FeaturedRecipes = recipes.Recipes.Take(ListSize).ToList(),
            RecentTips = tips.Tips.Take(ListSize).ToList()
        };
    }

    public async Task<NutritionistDashboardViewModel?> GetNutritionistDashboardAsync(int userId)
    {
        var profile = await _nutritionistService.GetProfileAsync(userId);
        if (profile is null) return null;

        var summaries = await _reviewRepository.GetSummariesAsync([userId]);
        var reviews = await _reviewRepository.GetByNutritionistAsync(userId);
        var conversations = await _chatService.GetConversationsAsync(userId);

        var displayName = string.IsNullOrWhiteSpace(profile.DisplayName)
            ? FirstName(profile.FullName)
            : profile.DisplayName;

        return new NutritionistDashboardViewModel
        {
            Id = profile.Id,
            DisplayName = displayName,
            Initials = Initials(displayName),
            MainSpecialty = profile.MainSpecialty,
            AvailableForNewPatients = profile.AvailableForNewPatients,
            AverageRating = summaries.TryGetValue(userId, out var s) ? Math.Round(s.Average, 1) : 0,
            ReviewCount = summaries.TryGetValue(userId, out var c) ? c.Count : 0,
            PublishedRecipes = await _recipeRepository.CountByAuthorAsync(userId),
            PublishedTips = await _tipRepository.CountByAuthorAsync(userId),
            UnreadMessages = await _chatService.GetUnreadCountAsync(userId),
            RecentReviews = reviews.Take(ListSize).Select(MapToReviewViewModel).ToList(),
            RecentConversations = conversations.Take(ListSize).ToList()
        };
    }

    private static NutritionistReviewViewModel MapToReviewViewModel(NutritionistReview r)
    {
        var name = r.IsAnonymous || string.IsNullOrWhiteSpace(r.Author.FullName)
            ? "Paciente"
            : r.Author.FullName;

        return new NutritionistReviewViewModel
        {
            Id = r.Id,
            Rating = r.Rating,
            Comment = r.Comment,
            IsAnonymous = r.IsAnonymous,
            AuthorName = name,
            AuthorInitials = Initials(name),
            CreatedAt = r.CreatedAt,
            CanDelete = false
        };
    }

    private static string FirstName(string fullName) =>
        fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? fullName;

    private static string Initials(string name) => string.Concat(
        name.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Take(2)
            .Select(w => char.ToUpper(w[0])));
}

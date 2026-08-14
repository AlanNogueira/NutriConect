using NutriConect.Business.Enums;

namespace NutriConect.Business.ViewModels;

public class RecipeDetailViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? ShortDescription { get; set; }
    public string? PhotoUrl { get; set; }
    public int? PrepMinutes { get; set; }
    public int? Servings { get; set; }
    public RecipeDifficultyEnum Difficulty { get; set; }
    public int? CaloriesPerServing { get; set; }
    public List<RecipeCategoryEnum> Categories { get; set; } = [];
    public bool ApprovedByNutritionist { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public List<string> Ingredients { get; set; } = [];
    public List<string> Steps { get; set; } = [];

    public bool CanEdit { get; set; }

    public double? AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public List<RatingBucketViewModel> Distribution { get; set; } = [];
    public List<RecipeReviewViewModel> Reviews { get; set; } = [];

    public bool CanReview { get; set; }

    public bool AlreadyReviewed { get; set; }

    public bool IsAuthor => CanEdit;

    public string AverageDisplay => AverageRating?.ToString("0.0") ?? "—";

    public string AuthorInitials => string.Concat(
        AuthorName.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                  .Take(2)
                  .Select(w => char.ToUpper(w[0])));
}

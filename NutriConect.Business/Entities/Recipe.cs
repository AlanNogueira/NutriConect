using NutriConect.Business.Enums;

namespace NutriConect.Business.Entities;

public class Recipe
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? ShortDescription { get; set; }
    public string? PhotoUrl { get; set; }
    public int? PrepMinutes { get; set; }
    public int? Servings { get; set; }
    public RecipeDifficultyEnum Difficulty { get; set; } = RecipeDifficultyEnum.Facil;
    public int? CaloriesPerServing { get; set; }

    public string? Categories { get; set; }

    public int AuthorId { get; set; }
    public User Author { get; set; } = null!;

    public bool ApprovedByNutritionist { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public ICollection<RecipeIngredient> Ingredients { get; set; } = [];
    public ICollection<RecipeStep> Steps { get; set; } = [];
}

using NutriConect.Business.Enums;

namespace NutriConect.Business.ViewModels;

public class RecipeCardViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
    public int? PrepMinutes { get; set; }
    public int? CaloriesPerServing { get; set; }
    public List<RecipeCategoryEnum> Categories { get; set; } = [];
    public bool ApprovedByNutritionist { get; set; }
    public string AuthorName { get; set; } = string.Empty;
}

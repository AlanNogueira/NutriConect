using NutriConect.Business.Enums;

namespace NutriConect.Business.ViewModels;

public class RecipeListViewModel
{
    public List<RecipeCardViewModel> Recipes { get; set; } = [];
    public RecipeCategoryEnum? Category { get; set; }
    public string? Query { get; set; }
    public RecipeSortEnum Sort { get; set; }
    public int Total => Recipes.Count;
}

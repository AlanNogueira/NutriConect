using NutriConect.Business.Enums;
using NutriConect.Business.InputModels;
using NutriConect.Business.ViewModels;

namespace NutriConect.Business.Interfaces.Services;

public interface IRecipeService
{
    Task<RecipeListViewModel> ListAsync(RecipeCategoryEnum? category, string? query, RecipeSortEnum sort);
    Task<RecipeDetailViewModel?> GetByIdAsync(int id, int? currentUserId);
    Task<RecipeInputModel?> GetForEditAsync(int id, int authorId);
    Task<int> CreateAsync(int authorId, RecipeInputModel input);
    Task<bool> UpdateAsync(int id, int authorId, RecipeInputModel input);
    Task<bool> DeleteAsync(int id, int authorId);
}

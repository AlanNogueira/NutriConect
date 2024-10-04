using NutriConect.Business.Entities;
using NutriConect.Business.InputModels;
using NutriConect.Business.Pagination;

namespace NutriConect.Business.Interfaces.Services
{
    public interface IRecipeService : IDisposable
    {
        Task Add(Recipe recipe);
        Task CreateRecipeEvaluation(RecipeEvaluation recipeEvaluation);
        Task<Recipe?> GetRecipeById(int id);
        Task <PaginatedList<Recipe>> GetRecipes(RecipeFilters filters, int page, int pageSize);
    }
}

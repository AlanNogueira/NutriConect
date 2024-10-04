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
        Task<Recipe?> GetRecipeByIdTracked(int id);
        Task <PaginatedList<Recipe>> GetRecipes(RecipeFilters filters, int page, int pageSize);
        Task<PaginatedList<Recipe>> GetRecipesByUser(string email, int page = 1, int pageSize = 10);
        Task UpdateRecipe(Recipe tip);
    }
}

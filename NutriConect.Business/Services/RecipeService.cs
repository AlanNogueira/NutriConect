using NutriConect.Business.Entities;
using NutriConect.Business.InputModels;
using NutriConect.Business.Interfaces.Repository;
using NutriConect.Business.Interfaces.Services;
using NutriConect.Business.Pagination;

namespace NutriConect.Business.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeEvaluationRepository _recipeEvaluationRepository;

        public void Dispose()
        {
            _recipeRepository?.Dispose();
        }

        public RecipeService(IRecipeRepository recipeRepository, IRecipeEvaluationRepository recipeEvaluationRepository)
        {
            _recipeRepository = recipeRepository;
            _recipeEvaluationRepository = recipeEvaluationRepository;
        }

        public async Task Add(Recipe recipe)
        {
            await _recipeRepository.Add(recipe);
        }

        public async Task<Recipe?> GetRecipeById(int id)
        {
            return await _recipeRepository.GetRecipeById(id);
        }

        public async Task<PaginatedList<Recipe>> GetRecipes(RecipeFilters filters, int page, int pageSize)
        {
            return await _recipeRepository.GetRecipes(filters, page, pageSize);
        }

        public async Task CreateRecipeEvaluation(RecipeEvaluation recipeEvaluation)
        {
            await _recipeEvaluationRepository.Add(recipeEvaluation);
        }

        public async Task<PaginatedList<Recipe>> GetRecipesByUser(string email, int page = 1, int pageSize = 10)
        {
            return await _recipeRepository.GetRecipesByUser(email, page, pageSize);
        }

        public async Task<Recipe?> GetRecipeByIdTracked(int id)
        {
            return await _recipeRepository.GetRecipeByIdTracked(id);
        }

        public async Task UpdateRecipe(Recipe recipe)
        {
            await _recipeRepository.Update(recipe);
        }
    }
}

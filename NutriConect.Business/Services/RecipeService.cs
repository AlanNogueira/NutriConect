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

        public void Dispose()
        {
            _recipeRepository?.Dispose();
        }
    }
}

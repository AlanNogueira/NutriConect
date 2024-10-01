using NutriConect.Business.Entities;
using NutriConect.Business.Interfaces.Repository;
using NutriConect.Business.Interfaces.Services;

namespace NutriConect.Business.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task Add(Recipe recipe)
        {
            await _recipeRepository.Add(recipe);
        }

        public void Dispose()
        {
            _recipeRepository?.Dispose();
        }
    }
}

using NutriConect.Business.Entities;
using NutriConect.Business.ViewModels;

namespace NutriConect.Business.Interfaces.Repositories;

public interface IRecipeReviewRepository : IRepository<RecipeReview>
{
    Task<List<RecipeReview>> GetByRecipeAsync(int recipeId);

    Task<bool> ExistsAsync(int recipeId, int authorId);

    Task<Dictionary<int, RecipeRatingSummary>> GetSummariesAsync(IEnumerable<int> recipeIds);
}

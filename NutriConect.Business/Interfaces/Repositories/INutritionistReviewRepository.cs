using NutriConect.Business.Entities;
using NutriConect.Business.ViewModels;

namespace NutriConect.Business.Interfaces.Repositories;

public interface INutritionistReviewRepository : IRepository<NutritionistReview>
{
    Task<List<NutritionistReview>> GetByNutritionistAsync(int nutritionistId);

    Task<bool> ExistsAsync(int nutritionistId, int authorId);

    Task<Dictionary<int, NutritionistRatingSummary>> GetSummariesAsync(IEnumerable<int> nutritionistIds);
}

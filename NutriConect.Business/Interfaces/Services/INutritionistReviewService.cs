using NutriConect.Business.InputModels;

namespace NutriConect.Business.Interfaces.Services;

public interface INutritionistReviewService
{
    Task<bool> CreateAsync(int authorId, CreateNutritionistReviewInputModel input);

    Task<bool> DeleteAsync(int reviewId, int authorId);
}

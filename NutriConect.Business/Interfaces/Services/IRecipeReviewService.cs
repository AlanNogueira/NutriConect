using NutriConect.Business.InputModels;

namespace NutriConect.Business.Interfaces.Services;

public interface IRecipeReviewService
{
    Task<bool> CreateAsync(int authorId, CreateRecipeReviewInputModel input);

    Task<bool> DeleteAsync(int reviewId, int authorId);
}

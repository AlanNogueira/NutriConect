using NutriConect.Business.Entities;
using NutriConect.Business.InputModels;
using NutriConect.Business.Interfaces;
using NutriConect.Business.Interfaces.Repositories;
using NutriConect.Business.Interfaces.Services;

namespace NutriConect.Business.Services;

public class RecipeReviewService : IRecipeReviewService
{
    private readonly IRecipeReviewRepository _reviewRepository;
    private readonly IRecipeRepository _recipeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RecipeReviewService(
        IRecipeReviewRepository reviewRepository,
        IRecipeRepository recipeRepository,
        IUnitOfWork unitOfWork)
    {
        _reviewRepository = reviewRepository;
        _recipeRepository = recipeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CreateAsync(int authorId, CreateRecipeReviewInputModel input)
    {
        if (input.Rating is < 1 or > 5)
            return false;

        var recipe = await _recipeRepository.GetByIdAsync(input.RecipeId);
        if (recipe is null)
            return false;

        if (recipe.AuthorId == authorId)
            return false;

        if (await _reviewRepository.ExistsAsync(input.RecipeId, authorId))
            return false;

        var review = new RecipeReview
        {
            RecipeId = input.RecipeId,
            AuthorId = authorId,
            Rating = input.Rating,
            Comment = string.IsNullOrWhiteSpace(input.Comment) ? null : input.Comment.Trim(),
            IsAnonymous = input.IsAnonymous
        };

        await _reviewRepository.AddAsync(review);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int reviewId, int authorId)
    {
        var review = await _reviewRepository.GetByIdAsync(reviewId);
        if (review is null || review.AuthorId != authorId)
            return false;

        _reviewRepository.Remove(review);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}

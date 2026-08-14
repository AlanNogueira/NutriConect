using NutriConect.Business.Entities;
using NutriConect.Business.InputModels;
using NutriConect.Business.Interfaces;
using NutriConect.Business.Interfaces.Repositories;
using NutriConect.Business.Interfaces.Services;

namespace NutriConect.Business.Services;

public class NutritionistReviewService : INutritionistReviewService
{
    private readonly INutritionistReviewRepository _reviewRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NutritionistReviewService(
        INutritionistReviewRepository reviewRepository,
        IRepository<User> userRepository,
        IUnitOfWork unitOfWork)
    {
        _reviewRepository = reviewRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CreateAsync(int authorId, CreateNutritionistReviewInputModel input)
    {
        if (input.Rating is < 1 or > 5)
            return false;

        var author = await _userRepository.GetByIdAsync(authorId);
        if (author is not Client)
            return false;

        var target = await _userRepository.GetByIdAsync(input.NutritionistId);
        if (target is not Nutritionist)
            return false;

        if (await _reviewRepository.ExistsAsync(input.NutritionistId, authorId))
            return false;

        var review = new NutritionistReview
        {
            NutritionistId = input.NutritionistId,
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

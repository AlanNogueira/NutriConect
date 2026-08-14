using Microsoft.EntityFrameworkCore;
using NutriConect.Business.Entities;
using NutriConect.Business.Interfaces.Repositories;
using NutriConect.Business.ViewModels;
using NutriConect.Data.Context;

namespace NutriConect.Data.Repositories;

public class NutritionistReviewRepository : Repository<NutritionistReview>, INutritionistReviewRepository
{
    public NutritionistReviewRepository(AppDbContext context) : base(context) { }

    public async Task<List<NutritionistReview>> GetByNutritionistAsync(int nutritionistId) =>
        await _context.Set<NutritionistReview>()
            .Include(r => r.Author)
            .Where(r => r.NutritionistId == nutritionistId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

    public async Task<bool> ExistsAsync(int nutritionistId, int authorId) =>
        await _context.Set<NutritionistReview>()
            .AnyAsync(r => r.NutritionistId == nutritionistId && r.AuthorId == authorId);

    public async Task<Dictionary<int, NutritionistRatingSummary>> GetSummariesAsync(IEnumerable<int> nutritionistIds)
    {
        var ids = nutritionistIds.Distinct().ToList();
        if (ids.Count == 0)
            return [];

        var rows = await _context.Set<NutritionistReview>()
            .Where(r => ids.Contains(r.NutritionistId))
            .GroupBy(r => r.NutritionistId)
            .Select(g => new { NutritionistId = g.Key, Average = g.Average(r => r.Rating), Count = g.Count() })
            .ToListAsync();

        return rows.ToDictionary(
            x => x.NutritionistId,
            x => new NutritionistRatingSummary(x.Average, x.Count));
    }
}

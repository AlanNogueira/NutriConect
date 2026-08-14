using Microsoft.EntityFrameworkCore;
using NutriConect.Business.Entities;
using NutriConect.Business.Interfaces.Repositories;
using NutriConect.Business.ViewModels;
using NutriConect.Data.Context;

namespace NutriConect.Data.Repositories;

public class RecipeReviewRepository : Repository<RecipeReview>, IRecipeReviewRepository
{
    public RecipeReviewRepository(AppDbContext context) : base(context) { }

    public async Task<List<RecipeReview>> GetByRecipeAsync(int recipeId) =>
        await _context.Set<RecipeReview>()
            .Include(r => r.Author)
            .Where(r => r.RecipeId == recipeId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

    public async Task<bool> ExistsAsync(int recipeId, int authorId) =>
        await _context.Set<RecipeReview>()
            .AnyAsync(r => r.RecipeId == recipeId && r.AuthorId == authorId);

    public async Task<Dictionary<int, RecipeRatingSummary>> GetSummariesAsync(IEnumerable<int> recipeIds)
    {
        var ids = recipeIds.Distinct().ToList();
        if (ids.Count == 0)
            return [];

        var rows = await _context.Set<RecipeReview>()
            .Where(r => ids.Contains(r.RecipeId))
            .GroupBy(r => r.RecipeId)
            .Select(g => new { RecipeId = g.Key, Average = g.Average(r => r.Rating), Count = g.Count() })
            .ToListAsync();

        return rows.ToDictionary(
            x => x.RecipeId,
            x => new RecipeRatingSummary(x.Average, x.Count));
    }
}

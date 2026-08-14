using Microsoft.EntityFrameworkCore;
using NutriConect.Business.Entities;
using NutriConect.Business.Enums;
using NutriConect.Business.Interfaces.Repositories;
using NutriConect.Data.Context;

namespace NutriConect.Data.Repositories;

public class RecipeRepository : Repository<Recipe>, IRecipeRepository
{
    public RecipeRepository(AppDbContext context) : base(context) { }

    public async Task<Recipe?> GetWithDetailsAsync(int id) =>
        await _context.Set<Recipe>()
            .Include(r => r.Author)
            .Include(r => r.Ingredients)
            .Include(r => r.Steps)
            .FirstOrDefaultAsync(r => r.Id == id);

    public async Task<IEnumerable<Recipe>> GetAllWithAuthorAsync(RecipeCategoryEnum? category, string? query)
    {
        var q = _context.Set<Recipe>()
            .Include(r => r.Author)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
            q = q.Where(r => r.Title.Contains(query) ||
                             (r.ShortDescription != null && r.ShortDescription.Contains(query)));

        if (category.HasValue)
        {
            var catName = category.Value.ToString();
            q = q.Where(r => r.Categories != null && r.Categories.Contains(catName));
        }

        return await q.OrderByDescending(r => r.CreatedAt).ToListAsync();
    }

    public async Task<int> CountByAuthorAsync(int authorId) =>
        await _context.Set<Recipe>().CountAsync(r => r.AuthorId == authorId);
}

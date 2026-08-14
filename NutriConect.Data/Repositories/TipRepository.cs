using Microsoft.EntityFrameworkCore;
using NutriConect.Business.Entities;
using NutriConect.Business.Enums;
using NutriConect.Business.Interfaces.Repositories;
using NutriConect.Data.Context;

namespace NutriConect.Data.Repositories;

public class TipRepository : Repository<Tip>, ITipRepository
{
    public TipRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Tip>> GetFeedAsync(TipCategoryEnum? category, bool onlyProfessionals)
    {
        var q = _context.Set<Tip>()
            .Include(t => t.Author)
            .Include(t => t.Comments)
                .ThenInclude(c => c.Author)
            .AsQueryable();

        if (category.HasValue)
            q = q.Where(t => t.Category == category.Value);

        if (onlyProfessionals)
            q = q.Where(t => t.ApprovedByNutritionist);

        return await q.OrderByDescending(t => t.CreatedAt).ToListAsync();
    }

    public async Task<Tip?> GetWithCommentsAsync(int id) =>
        await _context.Set<Tip>()
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(t => t.Id == id);

    public async Task<int> CountByAuthorAsync(int authorId) =>
        await _context.Set<Tip>().CountAsync(t => t.AuthorId == authorId);
}

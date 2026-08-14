using Microsoft.EntityFrameworkCore;
using NutriConect.Business.Entities;
using NutriConect.Business.Enums;
using NutriConect.Business.Interfaces.Repositories;
using NutriConect.Data.Context;

namespace NutriConect.Data.Repositories;

public class NutritionistRepository : Repository<Nutritionist>, INutritionistRepository
{
    public NutritionistRepository(AppDbContext context) : base(context) { }

    public async Task<Nutritionist?> GetWithCredentialsAsync(int id) =>
        await _context.Set<Nutritionist>()
            .Include(n => n.Credentials)
            .FirstOrDefaultAsync(n => n.Id == id);

    public async Task<IEnumerable<Nutritionist>> SearchAsync(string? query, NutritionistSpecialtyEnum? specialty, string? modality)
    {
        var q = _context.Set<Nutritionist>().AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
            q = q.Where(n => n.DisplayName.Contains(query) || n.ProfessionalTitle.Contains(query));

        if (specialty.HasValue)
        {
            var specName = specialty.Value.ToString();
            q = q.Where(n => n.MainSpecialty == specialty ||
                              (n.Specialties != null && n.Specialties.Contains(specName)));
        }

        q = modality switch
        {
            "online"   => q.Where(n => n.AcceptsOnline),
            "inperson" => q.Where(n => n.AcceptsInPerson),
            _          => q
        };

        return await q.OrderBy(n => n.DisplayName).ToListAsync();
    }
}

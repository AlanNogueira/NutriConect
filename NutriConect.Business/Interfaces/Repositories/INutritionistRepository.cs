using NutriConect.Business.Entities;
using NutriConect.Business.Enums;

namespace NutriConect.Business.Interfaces.Repositories;

public interface INutritionistRepository : IRepository<Nutritionist>
{
    Task<Nutritionist?> GetWithCredentialsAsync(int id);
    Task<IEnumerable<Nutritionist>> SearchAsync(string? query, NutritionistSpecialtyEnum? specialty, string? modality);
}

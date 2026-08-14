using NutriConect.Business.Entities;
using NutriConect.Business.Enums;

namespace NutriConect.Business.Interfaces.Repositories;

public interface IRecipeRepository : IRepository<Recipe>
{
    Task<Recipe?> GetWithDetailsAsync(int id);
    Task<IEnumerable<Recipe>> GetAllWithAuthorAsync(RecipeCategoryEnum? category, string? query);
    Task<int> CountByAuthorAsync(int authorId);
}

using NutriConect.Business.Entities;
using NutriConect.Business.Enums;

namespace NutriConect.Business.Interfaces.Repositories;

public interface ITipRepository : IRepository<Tip>
{
    Task<IEnumerable<Tip>> GetFeedAsync(TipCategoryEnum? category, bool onlyProfessionals);
    Task<Tip?> GetWithCommentsAsync(int id);
    Task<int> CountByAuthorAsync(int authorId);
}

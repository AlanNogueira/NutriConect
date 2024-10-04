using NutriConect.Business.Entities;
using NutriConect.Business.InputModels.Tip;
using NutriConect.Business.Pagination;

namespace NutriConect.Business.Interfaces.Services
{
    public interface ITipService : IDisposable
    {
        Task Add(Tip tip);
        Task CreateTipEvaluation(TipEvaluation recipeEvaluation);
        Task<Tip?> GetTipById(int id);
        Task<Tip> GetTipByIdTracked(int id);
        Task<PaginatedList<Tip>> GetTips(TipFilters filters, int page = 1, int pageSize = 10);
        Task<PaginatedList<Tip>> GetTipsByUser(string email, int page = 1, int pageSize = 10);
        Task UpdateTip(Tip tip);
    }
}

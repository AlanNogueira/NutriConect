using NutriConect.Business.Entities;
using NutriConect.Business.InputModels.Tip;
using NutriConect.Business.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.Interfaces.Repository
{
    public interface ITipRepository : IBaseRepository<Tip>
    {
        Task<Tip?> GetTipById(int id);
        Task<Tip> GetTipByIdTracked(int id);
        Task<PaginatedList<Tip>> GetTips(TipFilters filters, int page = 1, int pageSize = 10);
        Task<PaginatedList<Tip>> GetTipsByUser(string email, int page = 1, int pageSize = 10);
    }
}

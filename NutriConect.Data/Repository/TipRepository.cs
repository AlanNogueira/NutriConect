using Microsoft.EntityFrameworkCore;
using NutriConect.Business.Entities;
using NutriConect.Business.InputModels;
using NutriConect.Business.InputModels.Tip;
using NutriConect.Business.Interfaces.Repository;
using NutriConect.Business.Pagination;
using NutriConect.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Data.Repository
{
    public class TipRepository : BaseRepository<Tip>, ITipRepository
    {
        public TipRepository(ContextBase db) : base(db) { }

        public async Task<Tip?> GetTipById(int id)
        {
            return await Db.Tips.AsNoTracking()
                .Include(x => x.Client.Address)
                .Include(x => x.Client.User)
                .Include(x => x.Professional.Address)
                .Include(x => x.Professional.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tip> GetTipByIdTracked(int id)
        {
            return await Db.Tips
                .Include(x => x.Client.Address)
                .Include(x => x.Client.User)
                .Include(x => x.Professional.Address)
                .Include(x => x.Professional.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PaginatedList<Tip>> GetTips(TipFilters filters, int page = 1, int pageSize = 10)
        {
            return await PaginatedList<Tip>.CreateAsync(TipFilters(filters), page, pageSize);
        }

        public async Task<PaginatedList<Tip>> GetTipsByUser(string email, int page = 1, int pageSize = 10)
        {
            return await PaginatedList<Tip>.CreateAsync(Db.Tips.Where(x => x.Client.User.Email == email || x.Professional.User.Email == email), page, pageSize);
        }

        public IQueryable<Tip> TipFilters(TipFilters filters)
        {
            return Db.Tips
                .Include(x => x.Client.Address)
                .Include(x => x.Client.User)
                .Include(x => x.Professional.Address)
                .Include(x => x.Professional.User)
                .Where(x => !string.IsNullOrEmpty(filters.UserName) ? (x.Client.Name.ToUpper().Contains(filters.UserName.ToUpper()) || x.Professional.Name.ToUpper().Contains(filters.UserName.ToUpper())) : true);
        }
    }
}

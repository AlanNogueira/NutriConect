using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NutriConect.Business.Entities;
using NutriConect.Business.InputModels.Filters;
using NutriConect.Business.Interfaces.Repository;
using NutriConect.Business.Pagination;
using NutriConect.Data.Context;

namespace NutriConect.Data.Repository
{
    public class ProfessionalRepository : BaseRepository<Professional>, IProfessionalRepository
    {
        public ProfessionalRepository(ContextBase db) : base (db) {  }

        public Task CreateProfessionalEvaluation(ProfessionalEvaluation professionalEvaluation)
        {
            throw new NotImplementedException();
        }

        public async Task<Professional?> FindByIdTracked(int Id)
        {
            return await Db.Professionals.AsTracking().Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Professional?> GetProfessionalByEmail(string email)
        {
            return await Db.Professionals.Include(x => x.Address).Include(x => x.User).FirstOrDefaultAsync(x => x.User.Email == email);
        }

        public async Task<Professional?> GetProfessionalByIdNoTracking(int professionalId)
        {
            return await Db.Professionals.AsNoTracking().Include(x => x.User).Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == professionalId);
        }

        public async Task<PaginatedList<Professional>> GetProfessionals(ProfessionalFilters filters, int page = 1, int pageSize = 10)
        {
            return await PaginatedList<Professional>.CreateAsync(ProfessionalsFilters(filters), page, pageSize);
        }

        public IQueryable<Professional> ProfessionalsFilters(ProfessionalFilters filters)
        {
            return Db.Professionals.Include(x => x.Address).Where(x => !string.IsNullOrEmpty(filters.City) ? x.Address.City.ToUpper().Contains(filters.City.ToUpper()) : true)
                                   .Where(x => !string.IsNullOrEmpty(filters.State) ? x.Address.State.ToUpper().Contains(filters.State.ToUpper()) : true);
        }
    }
}

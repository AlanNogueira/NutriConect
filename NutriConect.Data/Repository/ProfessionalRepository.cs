using Microsoft.EntityFrameworkCore;
using NutriConect.Business.Entities;
using NutriConect.Business.Interfaces.Repository;
using NutriConect.Data.Context;

namespace NutriConect.Data.Repository
{
    public class ProfessionalRepository : BaseRepository<Professional>, IProfessionalRepository
    {
        public ProfessionalRepository(ContextBase db) : base (db) {  }

        public async Task<Professional?> FindByIdTracked(int Id)
        {
            return await Db.Professionals.AsTracking().FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Professional?> GetClientByEmail(string email)
        {
            return await Db.Professionals.Include(x => x.Address).Include(x => x.User).FirstOrDefaultAsync(x => x.User.Email == email);
        }
    }
}

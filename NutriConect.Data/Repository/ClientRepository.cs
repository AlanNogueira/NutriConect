using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using NutriConect.Business.Entities;
using NutriConect.Business.Interfaces.Repository;
using NutriConect.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Data.Repository
{
    public class ClientRepository :BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(ContextBase db) : base (db) { }

        public async Task<Client?> FindByIdTracked(int Id)
        {
            return await Db.Clients.AsTracking().FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}

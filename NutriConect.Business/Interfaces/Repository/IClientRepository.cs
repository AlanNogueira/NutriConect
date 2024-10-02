using NutriConect.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.Interfaces.Repository
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        Task<Client?> FindByIdTracked(int Id);
        Task<Client?> GetClientByEmail(string email);
    }
}

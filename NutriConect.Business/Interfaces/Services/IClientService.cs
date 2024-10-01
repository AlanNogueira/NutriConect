using NutriConect.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.Interfaces.Services
{
    public interface IClientService : IDisposable
    {
        Task<IEnumerable<Client>> ListAll();
        Task<Client?> FindByIdTracked(int Id);
        Task Add(Client client);
    }
}

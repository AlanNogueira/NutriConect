using NutriConect.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.Interfaces.Services
{
    public interface IProfessionalService : IDisposable
    {
        Task<IEnumerable<Professional>> ListAll();
        Task<Professional?> FindByIdTracked(int id);
        Task Add(Professional professional);
        Task Update(Professional professional);
        Task<Professional?> GetProfessionalByEmail(string email);
    }
}

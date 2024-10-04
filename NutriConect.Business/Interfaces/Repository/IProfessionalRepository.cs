using NutriConect.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.Interfaces.Repository
{
    public interface IProfessionalRepository : IBaseRepository<Professional>
    {
        Task<Professional?> FindByIdTracked(int Id);
        Task<Professional?> GetClientByEmail(string Email);
    }
}

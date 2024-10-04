using NutriConect.Business.Entities;
using NutriConect.Business.InputModels.Filters;
using NutriConect.Business.Pagination;
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
        Task<PaginatedList<Professional>> GetProfessionals(ProfessionalFilters filters, int page = 1, int pageSize = 10);
    }
}

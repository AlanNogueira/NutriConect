using NutriConect.Business.Entities;
using NutriConect.Business.InputModels.Filters;
using NutriConect.Business.Pagination;
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
        Task<Professional?> GetProfessionalByEmail(string Email);
        Task<PaginatedList<Professional>> GetProfessionals(ProfessionalFilters filters, int page = 1, int pageSize = 10);
    }
}

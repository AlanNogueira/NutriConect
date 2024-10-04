using NutriConect.Business.Entities;
using NutriConect.Business.InputModels;
using NutriConect.Business.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.Interfaces.Repository
{
    public interface IRecipeRepository : IBaseRepository<Recipe>
    {
        public Task<Recipe?> GetRecipeById(int id);
        Task<PaginatedList<Recipe>> GetRecipes(RecipeFilters filters, int page, int pageSize);
    }
}

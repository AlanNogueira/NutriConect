using Microsoft.EntityFrameworkCore;
using NutriConect.Business.Entities;
using NutriConect.Business.InputModels;
using NutriConect.Business.Interfaces.Repository;
using NutriConect.Business.Pagination;
using NutriConect.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Data.Repository
{
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ContextBase db) : base(db) { }

        public async Task<Recipe?> GetRecipeById(int id)
        {
            return await Db.Recipes
                .Include(x => x.Client.Address)
                .Include(x => x.Client.User)
                .Include(x => x.Professional.Address)
                .Include(x => x.Professional.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PaginatedList<Recipe>> GetRecipes(RecipeFilters filters, int page, int pageSize)
        {
            return await PaginatedList<Recipe>.CreateAsync(RecipeFilters(filters), page, pageSize);
        }

        public IQueryable<Recipe> RecipeFilters(RecipeFilters recipeFilters)
        {
            return Db.Recipes
                .Include(x => x.Client.Address)
                .Include(x => x.Client.User)
                .Include(x => x.Professional.Address)
                .Include(x => x.Professional.User)
                .Where(x => !string.IsNullOrEmpty(recipeFilters.UserName) ? (x.Client.Name.ToUpper().Contains(recipeFilters.UserName.ToUpper()) || x.Professional.Name.ToUpper().Contains(recipeFilters.UserName.ToUpper())) : true);
        }
    }
}

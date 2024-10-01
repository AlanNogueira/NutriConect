using NutriConect.Business.Entities;
using NutriConect.Business.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.Mappings
{
    public static class RecipeMapping
    {
        public static Recipe CreateRecipeToRecipe(CreateRecipeInputModel createRecipe, ApplicationUser user)
        {
            return new Recipe
            {
                Name = createRecipe.Name,
                Text = createRecipe.Text,
                CreateDate = DateTime.Now,
                User = user,
            };
        }
    }
}

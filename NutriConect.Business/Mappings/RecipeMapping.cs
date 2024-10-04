using NutriConect.Business.Entities;
using NutriConect.Business.InputModels;
using NutriConect.Business.InputModels.Recipe;
using NutriConect.Business.ViewModels;
using NutriConect.Business.ViewModels.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.Mappings
{
    public static class RecipeMapping
    {
        public static Recipe CreateRecipeToRecipe(CreateRecipeInputModel createRecipe, Client? client = null, Professional? professional = null)
        {
            return new Recipe
            {
                Name = createRecipe.Name,
                Text = createRecipe.Text,
                CreateDate = DateTime.Now,
                Client = client,
                Professional = professional
            };
        }

        public static RecipeViewModel RecipeToViewModel(Recipe recipe)
        {
            return new RecipeViewModel
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Text = recipe.Text,
                Client = ClientMapping.ClienteToViewModel(recipe.Client),
                Professional = ProfessionalMapping.ProfessionalToViewModel(recipe.Professional)
            };
        }

        public static RecipeEvaluation CreateRecipeEvaluationToRecipeEvaluation(RecipeEvaluationsInputModel createRecipeEvaluations, Recipe recipe, Client? client = null, Professional? professional = null)
        {
            return new RecipeEvaluation
            {
                Title = createRecipeEvaluations.Title,
                Text = createRecipeEvaluations.Text,
                Value = createRecipeEvaluations.Value,
                Recipe = recipe,
                Client = client,
                Professional = professional
            };
        }

        public static RecipeByUser RecipeToReciberByUserViewModel(Recipe recipe)
        {
            return new RecipeByUser
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Text = recipe.Text
            };
        }

        public static void UpdateRecipeToRecipe(UpdateRecipeInputMode updateRecipe, Recipe recipe)
        {
            recipe.Name = updateRecipe.Title;
            recipe.Text = updateRecipe.Text;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.InputModels.Recipe
{
    public class UpdateRecipeInputMode
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
    }
}

using NutriConect.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.ViewModels
{
    public class RecipeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public ClientViewModel? Client { get; set; }
        public ProfessionalViewModel? Professional { get; set; }
    }
}

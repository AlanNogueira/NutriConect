using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.ViewModels.Professional
{
    public class ProfessionalEvaluationViewModel
    {
        public string Text { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }
        public int ClientId { get; set; }
        public int ProfessionalId { get; set; }
    }
}

using NutriConect.Business.ViewModels.Professional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.ViewModels
{
    public class ProfessionalViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public AddressViewModel Address { get; set; }
        public UserViewModel User { get; set; }
        public int AverageEvaluation { get; set; }
        public List<ProfessionalEvaluationViewModel> Evaluations { get; set; }
    }
}

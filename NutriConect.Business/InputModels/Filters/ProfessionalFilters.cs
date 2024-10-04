using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.InputModels.Filters
{
    public class ProfessionalFilters
    {
        [DefaultValue("")]
        public string? State { get; set; }

        [DefaultValue("")]
        public string? City { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.Entities
{
    public class Recipe : BaseEntity
    {
        public string Name { get; set; }
        public ApplicationUser User { get; set; }
        public string Text { get; set; }
    }
}

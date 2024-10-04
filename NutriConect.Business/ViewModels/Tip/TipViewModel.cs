using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.ViewModels.Tip
{
    public class TipViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public ClientViewModel? Client { get; set; }
        public ProfessionalViewModel? Professional { get; set; }
    }
}

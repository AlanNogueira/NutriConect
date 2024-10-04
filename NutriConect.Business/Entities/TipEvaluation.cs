using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.Entities
{
    public class TipEvaluation : BaseEntity
    {
        public string Text { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }
        public Tip Tip { get; set; }
        public Client? Client { get; set; }
        public Professional? Professional { get; set; }
    }
}

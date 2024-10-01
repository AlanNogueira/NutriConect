using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public ApplicationUser User { get; set; }
    }
}

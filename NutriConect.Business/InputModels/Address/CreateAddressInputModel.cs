using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.InputModels
{
    public class CreateAddressInputModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string City { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string State { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Street { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Number { get; set; }
    }
}

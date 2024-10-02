using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.InputModels
{
    public class UpdatePasswordInputModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} é inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campor {0} deve ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campor {0} deve ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Compare(nameof(NewPassword), ErrorMessage = "As senhas devem ser iguais.")]
        public string ConfirmNewPassword { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace NutriConect.Business.InputModels
{
    public class CreateTipInputModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Text { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Title { get; set; }
    }
}

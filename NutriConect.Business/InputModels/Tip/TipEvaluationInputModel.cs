using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.InputModels.Tip
{
    public class TipEvaluationInputModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Text { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Title { get; set; }

        [Range(1, 5, ErrorMessage = "O valor mínimo é 1 e o máximo é 5.")]
        public int Value { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int RecipeId { get; set; }
    }
}

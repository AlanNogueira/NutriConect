using System.ComponentModel.DataAnnotations;
using NutriConect.Business.Enums;

namespace NutriConect.Business.InputModels;

public class CreateTipInputModel
{
    [Required(ErrorMessage = "Escreva sua dica antes de compartilhar.")]
    [MaxLength(1000)]
    public string Body { get; set; } = string.Empty;

    public TipCategoryEnum? Category { get; set; }
}

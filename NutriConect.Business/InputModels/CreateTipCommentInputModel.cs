using System.ComponentModel.DataAnnotations;

namespace NutriConect.Business.InputModels;

public class CreateTipCommentInputModel
{
    public int TipId { get; set; }

    [Required(ErrorMessage = "Escreva um comentário.")]
    [MaxLength(800)]
    public string Body { get; set; } = string.Empty;
}

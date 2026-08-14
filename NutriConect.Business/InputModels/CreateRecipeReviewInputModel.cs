using System.ComponentModel.DataAnnotations;

namespace NutriConect.Business.InputModels;

public class CreateRecipeReviewInputModel
{
    [Required]
    public int RecipeId { get; set; }

    [Range(1, 5, ErrorMessage = "Escolha uma nota de 1 a 5 estrelas.")]
    public int Rating { get; set; }

    [MaxLength(1000, ErrorMessage = "O comentário deve ter no máximo 1000 caracteres.")]
    public string? Comment { get; set; }

    public bool IsAnonymous { get; set; }
}

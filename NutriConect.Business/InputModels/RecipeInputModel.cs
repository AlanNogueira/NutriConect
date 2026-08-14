using System.ComponentModel.DataAnnotations;
using NutriConect.Business.Enums;

namespace NutriConect.Business.InputModels;

public class RecipeInputModel
{
    [Required(ErrorMessage = "O título da receita é obrigatório.")]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? ShortDescription { get; set; }

    [Range(1, 1000, ErrorMessage = "Informe um tempo de preparo válido (em minutos).")]
    public int? PrepMinutes { get; set; }

    [Range(1, 100, ErrorMessage = "Informe um número de porções válido.")]
    public int? Servings { get; set; }

    public RecipeDifficultyEnum Difficulty { get; set; } = RecipeDifficultyEnum.Facil;

    [Range(0, 10000, ErrorMessage = "Informe um valor de calorias válido.")]
    public int? CaloriesPerServing { get; set; }

    public List<RecipeCategoryEnum> Categories { get; set; } = [];

    public List<string> Ingredients { get; set; } = [];

    public List<string> Steps { get; set; } = [];
}

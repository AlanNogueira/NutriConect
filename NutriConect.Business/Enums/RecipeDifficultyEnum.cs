using System.ComponentModel.DataAnnotations;

namespace NutriConect.Business.Enums;

public enum RecipeDifficultyEnum
{
    [Display(Name = "Fácil")]    Facil    = 0,
    [Display(Name = "Médio")]    Medio    = 1,
    [Display(Name = "Avançado")] Avancado = 2
}

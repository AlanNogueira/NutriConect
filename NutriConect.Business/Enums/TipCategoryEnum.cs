using System.ComponentModel.DataAnnotations;

namespace NutriConect.Business.Enums;

public enum TipCategoryEnum
{
    [Display(Name = "Hidratação")] Hidratacao = 0,
    [Display(Name = "Receitas")]   Receitas   = 1,
    [Display(Name = "Treino")]     Treino     = 2,
    [Display(Name = "Rotina")]     Rotina     = 3
}

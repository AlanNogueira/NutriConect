using System.ComponentModel.DataAnnotations;

namespace NutriConect.Business.Enums;

public enum RecipeSortEnum
{
    [Display(Name = "Mais recentes")]    Recentes         = 0,
    [Display(Name = "Melhores avaliadas")] MelhorAvaliadas = 1
}

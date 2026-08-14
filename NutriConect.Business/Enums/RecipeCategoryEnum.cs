using System.ComponentModel.DataAnnotations;

namespace NutriConect.Business.Enums;

public enum RecipeCategoryEnum
{
    [Display(Name = "Café da manhã")]    CafeDaManha    = 0,
    [Display(Name = "Almoço")]           Almoco         = 1,
    [Display(Name = "Jantar")]           Jantar         = 2,
    [Display(Name = "Lanche")]           Lanche         = 3,
    [Display(Name = "Low carb")]         LowCarb        = 4,
    [Display(Name = "Vegetariana")]      Vegetariana    = 5,
    [Display(Name = "Rica em proteína")] RicaEmProteina = 6
}

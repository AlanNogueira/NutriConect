using System.ComponentModel.DataAnnotations;

namespace NutriConect.Business.Enums;

public enum NutritionistSpecialtyEnum
{
    [Display(Name = "Nutrição esportiva")]  NutricaoEsportiva   = 0,
    [Display(Name = "Clínica")]             Clinica             = 1,
    [Display(Name = "Materno-infantil")]    MaternoInfantil     = 2,
    [Display(Name = "Emagrecimento")]       Emagrecimento       = 3,
    [Display(Name = "Reeducação alimentar")]ReeducacaoAlimentar = 4,
    [Display(Name = "Vegetariana/Vegana")]  VegetarianaVegana   = 5,
    [Display(Name = "Hipertrofia")]         Hipertrofia         = 6,
    [Display(Name = "Low carb")]            LowCarb             = 7
}

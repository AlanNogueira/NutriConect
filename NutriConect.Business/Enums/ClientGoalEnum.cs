using System.ComponentModel.DataAnnotations;

namespace NutriConect.Business.Enums;

public enum ClientGoalEnum
{
    [Display(Name = "Reeducação alimentar")] ReeducacaoAlimentar = 0,
    [Display(Name = "Emagrecimento")]        Emagrecimento       = 1,
    [Display(Name = "Ganho de massa")]       GanhoDeMassa        = 2,
    [Display(Name = "Nutrição esportiva")]   NutricaoEsportiva   = 3
}

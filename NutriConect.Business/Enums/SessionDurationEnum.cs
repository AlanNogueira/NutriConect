using System.ComponentModel.DataAnnotations;

namespace NutriConect.Business.Enums;

public enum SessionDurationEnum
{
    [Display(Name = "40 minutos")] Quarenta  = 40,
    [Display(Name = "50 minutos")] Cinquenta = 50,
    [Display(Name = "60 minutos")] Sessenta  = 60
}

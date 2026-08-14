using System.ComponentModel.DataAnnotations;

namespace NutriConect.Business.Enums;

public enum UserRoleEnum
{
    [Display(Name = "Cliente")]      Cliente      = 0,
    [Display(Name = "Profissional")] Profissional = 1
}

using System.ComponentModel.DataAnnotations;

namespace NutriConect.Business.Enums;

public enum ContactSubjectEnum
{
    [Display(Name = "Dúvida sobre planos")]  DuvidaSobrePlanos  = 0,
    [Display(Name = "Disponibilidade")]      Disponibilidade    = 1,
    [Display(Name = "Convênio")]             Convenio           = 2,
    [Display(Name = "Outro")]                Outro              = 3
}

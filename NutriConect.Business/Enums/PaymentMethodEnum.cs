using System.ComponentModel.DataAnnotations;

namespace NutriConect.Business.Enums;

public enum PaymentMethodEnum
{
    [Display(Name = "Pix")]               Pix           = 0,
    [Display(Name = "Cartão de crédito")] CartaoCredito = 1,
    [Display(Name = "Convênio")]          Convenio      = 2
}

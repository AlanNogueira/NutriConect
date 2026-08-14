using System.ComponentModel.DataAnnotations;
using NutriConect.Business.Enums;

namespace NutriConect.Business.InputModels;

public class RegisterInputModel : IValidatableObject
{
    public UserRoleEnum Role { get; set; } = UserRoleEnum.Cliente;

    [Required(ErrorMessage = "O nome completo é obrigatório.")]
    [StringLength(150, ErrorMessage = "O nome não pode ter mais de 150 caracteres.")]
    public string FullName { get; set; } = string.Empty;

    [StringLength(20, ErrorMessage = "O CRN não pode ter mais de 20 caracteres.")]
    public string? CrnRegistration { get; set; }

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "A confirmação de senha é obrigatória.")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "As senhas não conferem.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Range(typeof(bool), "true", "true", ErrorMessage = "Você deve aceitar os Termos de Uso.")]
    public bool AcceptedTerms { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Role == UserRoleEnum.Profissional && string.IsNullOrWhiteSpace(CrnRegistration))
            yield return new ValidationResult(
                "O registro CRN é obrigatório para profissionais.",
                [nameof(CrnRegistration)]);
    }
}

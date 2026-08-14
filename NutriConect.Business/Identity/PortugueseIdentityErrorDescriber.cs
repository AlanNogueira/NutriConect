using Microsoft.AspNetCore.Identity;

namespace NutriConect.Business.Identity;

public class PortugueseIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateUserName(string userName) => new()
    {
        Code = nameof(DuplicateUserName),
        Description = $"O e-mail '{userName}' já está cadastrado."
    };

    public override IdentityError DuplicateEmail(string email) => new()
    {
        Code = nameof(DuplicateEmail),
        Description = $"O e-mail '{email}' já está cadastrado."
    };

    public override IdentityError PasswordTooShort(int length) => new()
    {
        Code = nameof(PasswordTooShort),
        Description = $"A senha deve ter no mínimo {length} caracteres."
    };

    public override IdentityError PasswordRequiresDigit() => new()
    {
        Code = nameof(PasswordRequiresDigit),
        Description = "A senha deve conter ao menos um número (0–9)."
    };

    public override IdentityError PasswordRequiresLower() => new()
    {
        Code = nameof(PasswordRequiresLower),
        Description = "A senha deve conter ao menos uma letra minúscula."
    };

    public override IdentityError PasswordRequiresUpper() => new()
    {
        Code = nameof(PasswordRequiresUpper),
        Description = "A senha deve conter ao menos uma letra maiúscula."
    };

    public override IdentityError PasswordRequiresNonAlphanumeric() => new()
    {
        Code = nameof(PasswordRequiresNonAlphanumeric),
        Description = "A senha deve conter ao menos um caractere especial."
    };

    public override IdentityError InvalidEmail(string? email) => new()
    {
        Code = nameof(InvalidEmail),
        Description = $"O e-mail '{email}' não é válido."
    };

    public override IdentityError InvalidUserName(string? userName) => new()
    {
        Code = nameof(InvalidUserName),
        Description = $"O e-mail '{userName}' contém caracteres inválidos."
    };
}

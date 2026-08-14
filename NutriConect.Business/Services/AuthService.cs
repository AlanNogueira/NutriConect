using Microsoft.AspNetCore.Identity;
using NutriConect.Business.Entities;
using NutriConect.Business.Enums;
using NutriConect.Business.InputModels;
using NutriConect.Business.Interfaces.Services;

namespace NutriConect.Business.Services;

public class AuthService : IAuthService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AuthService(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<bool> LoginAsync(string email, string password, bool rememberMe)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
        return result.Succeeded;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> RegisterAsync(RegisterInputModel input)
    {
        User user = input.Role == UserRoleEnum.Profissional
            ? new Nutritionist { CrnRegistration = input.CrnRegistration ?? string.Empty }
            : new Client();

        user.FullName = input.FullName;
        user.UserName = input.Email;
        user.Email = input.Email;
        user.AcceptedTerms = input.AcceptedTerms;

        var result = await _userManager.CreateAsync(user, input.Password);

        if (!result.Succeeded)
            return (false, result.Errors.Select(e => e.Description));

        var role = user is Nutritionist ? "Profissional" : "Cliente";
        await _userManager.AddToRoleAsync(user, role);

        await _signInManager.SignInAsync(user, isPersistent: false);
        return (true, []);
    }
}

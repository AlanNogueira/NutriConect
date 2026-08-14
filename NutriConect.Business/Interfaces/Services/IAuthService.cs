using NutriConect.Business.InputModels;

namespace NutriConect.Business.Interfaces.Services;

public interface IAuthService
{
    Task<bool> LoginAsync(string email, string password, bool rememberMe);
    Task LogoutAsync();
    Task<(bool Succeeded, IEnumerable<string> Errors)> RegisterAsync(RegisterInputModel input);
}

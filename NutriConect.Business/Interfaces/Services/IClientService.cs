using NutriConect.Business.InputModels;
using NutriConect.Business.ViewModels;

namespace NutriConect.Business.Interfaces.Services;

public interface IClientService
{
    Task<ClientProfileViewModel?> GetProfileAsync(int userId);
    Task UpdateProfileAsync(int userId, UpdateClientProfileInputModel input);
}

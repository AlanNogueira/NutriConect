using NutriConect.Business.InputModels;
using NutriConect.Business.Interfaces;
using NutriConect.Business.Interfaces.Repositories;
using NutriConect.Business.Interfaces.Services;
using NutriConect.Business.ViewModels;

namespace NutriConect.Business.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ClientProfileViewModel?> GetProfileAsync(int userId)
    {
        var client = await _clientRepository.GetByIdAsync(userId);
        if (client is null) return null;

        return new ClientProfileViewModel
        {
            Id = client.Id,
            FullName = client.FullName,
            Email = client.Email ?? string.Empty,
            Phone = client.Phone,
            City = client.City,
            BirthDate = client.BirthDate,
            MainGoal = client.MainGoal,
            DietaryPreference = client.DietaryPreference,
            About = client.About
        };
    }

    public async Task UpdateProfileAsync(int userId, UpdateClientProfileInputModel input)
    {
        var client = await _clientRepository.GetByIdAsync(userId);
        if (client is null) return;

        client.FullName = input.FullName;
        client.Phone = input.Phone;
        client.City = input.City;
        client.BirthDate = input.BirthDate;
        client.MainGoal = input.MainGoal;
        client.DietaryPreference = input.DietaryPreference;
        client.About = input.About;

        _clientRepository.Update(client);
        await _unitOfWork.SaveChangesAsync();
    }
}

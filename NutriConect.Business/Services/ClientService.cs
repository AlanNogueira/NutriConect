using NutriConect.Business.Entities;
using NutriConect.Business.Interfaces.Repository;
using NutriConect.Business.Interfaces.Services;
using System;

namespace NutriConect.Business.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<Client>> ListAll()
        {
            return await _clientRepository.FindAll();
        }

        public async Task<Client?> FindByIdTracked(int Id)
        {
            return await _clientRepository.FindByIdTracked(Id);
        }

        public void Dispose()
        {
            _clientRepository?.Dispose();
        }
    }
}

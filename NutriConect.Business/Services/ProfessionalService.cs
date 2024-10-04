using NutriConect.Business.Entities;
using NutriConect.Business.Interfaces.Repository;
using NutriConect.Business.Interfaces.Services;

namespace NutriConect.Business.Services
{
    public class ProfessionalService : IProfessionalService
    {
        private readonly IProfessionalRepository _professionalRepository;

        public ProfessionalService(IProfessionalRepository professionalRepository)
        {
            _professionalRepository = professionalRepository;
        }

        public async Task<IEnumerable<Professional>> ListAll()
        {
            return await _professionalRepository.FindAll();
        }

        public async Task<Professional?> FindByIdTracked(int id)
        {
            return await _professionalRepository.FindByIdTracked(id);
        }

        public async Task Add(Professional professional)
        {
            await _professionalRepository.Add(professional);
        }

        public async Task Update(Professional professional)
        {
            await _professionalRepository.Update(professional);
        }

        public async Task<Professional?> GetClientByEmail(string email)
        {
            return await _professionalRepository.GetClientByEmail(email);
        }

        public void Dispose()
        {
            _professionalRepository?.Dispose();
        }
    }
}

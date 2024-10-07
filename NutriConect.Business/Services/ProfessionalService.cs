using NutriConect.Business.Entities;
using NutriConect.Business.InputModels.Filters;
using NutriConect.Business.Interfaces.Repository;
using NutriConect.Business.Interfaces.Services;
using NutriConect.Business.Pagination;

namespace NutriConect.Business.Services
{
    public class ProfessionalService : IProfessionalService
    {
        private readonly IProfessionalRepository _professionalRepository;
        private readonly IProfessionalEvaluationRepository _professionalEvaluationRepository;

        public ProfessionalService(IProfessionalRepository professionalRepository, IProfessionalEvaluationRepository professionalEvaluationRepository)
        {
            _professionalRepository = professionalRepository;
            _professionalEvaluationRepository = professionalEvaluationRepository;
        }

        public void Dispose()
        {
            _professionalRepository?.Dispose();
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

        public async Task<Professional?> GetProfessionalByEmail(string email)
        {
            return await _professionalRepository.GetProfessionalByEmail(email);
        }

        public async Task<PaginatedList<Professional>> GetProfessionals(ProfessionalFilters filters, int page = 1, int pageSize = 10)
        {
            return await _professionalRepository.GetProfessionals(filters, page, pageSize);
        }

        public async Task CreateProfessionalEvaluation(ProfessionalEvaluation professionalEvaluation)
        {
            await _professionalEvaluationRepository.Add(professionalEvaluation);
        }

        public async Task<Professional?> GetProfessionalByIdNoTracking(int professionalId)
        {
            return await _professionalRepository.GetProfessionalByIdNoTracking(professionalId);
        }
    }
}

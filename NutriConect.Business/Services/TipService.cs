using NutriConect.Business.Entities;
using NutriConect.Business.InputModels.Tip;
using NutriConect.Business.Interfaces.Repository;
using NutriConect.Business.Interfaces.Services;
using NutriConect.Business.Pagination;

namespace NutriConect.Business.Services
{
    public class TipService : ITipService
    {
        private readonly ITipRepository _tipRepository;
        private readonly ITipEvaluationRepository _tipEvaluationRepository;

        public void Dispose()
        {
            _tipRepository?.Dispose();
        }

        public TipService(ITipRepository tipRepository)
        {
            _tipRepository = tipRepository;
        }

        public async Task Add(Tip tip)
        {
            await _tipRepository.Add(tip);
        }

        public async Task<PaginatedList<Tip>> GetTips(TipFilters filters, int page = 1, int pageSize = 10)
        {
            return await _tipRepository.GetTips(filters, page, pageSize);
        }

        public async Task<Tip?> GetTipById(int id)
        {
            return await _tipRepository.GetTipById(id);
        }

        public async Task CreateTipEvaluation(TipEvaluation recipeEvaluation)
        {
            await _tipEvaluationRepository.Add(recipeEvaluation);
        }

        public async Task<PaginatedList<Tip>> GetTipsByUser(string email, int page = 1, int pageSize = 10)
        {
            return await _tipRepository.GetTipsByUser(email, page, pageSize);
        }

        public async Task<Tip> GetTipByIdTracked(int id)
        {
            return await _tipRepository.GetTipByIdTracked(id);
        }

        public async Task UpdateTip(Tip tip)
        {
            await _tipRepository.Update(tip);
        }
    }
}

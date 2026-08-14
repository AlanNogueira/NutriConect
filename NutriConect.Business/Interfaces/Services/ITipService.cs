using NutriConect.Business.Enums;
using NutriConect.Business.InputModels;
using NutriConect.Business.ViewModels;

namespace NutriConect.Business.Interfaces.Services;

public interface ITipService
{
    Task<TipFeedViewModel> ListAsync(TipCategoryEnum? category, bool onlyProfessionals, int? currentUserId);
    Task<int> CreateAsync(int authorId, CreateTipInputModel input);
    Task<bool> AddCommentAsync(int authorId, CreateTipCommentInputModel input);
    Task<bool> DeleteAsync(int id, int authorId);
}

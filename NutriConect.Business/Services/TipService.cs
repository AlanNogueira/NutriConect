using NutriConect.Business.Entities;
using NutriConect.Business.Enums;
using NutriConect.Business.InputModels;
using NutriConect.Business.Interfaces;
using NutriConect.Business.Interfaces.Repositories;
using NutriConect.Business.Interfaces.Services;
using NutriConect.Business.ViewModels;

namespace NutriConect.Business.Services;

public class TipService : ITipService
{
    private readonly ITipRepository _tipRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TipService(
        ITipRepository tipRepository,
        IRepository<User> userRepository,
        IUnitOfWork unitOfWork)
    {
        _tipRepository = tipRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TipFeedViewModel> ListAsync(TipCategoryEnum? category, bool onlyProfessionals, int? currentUserId)
    {
        var tips = await _tipRepository.GetFeedAsync(category, onlyProfessionals);

        return new TipFeedViewModel
        {
            Category = category,
            OnlyProfessionals = onlyProfessionals,
            Tips = tips.Select(t => new TipCardViewModel
            {
                Id = t.Id,
                Body = t.Body,
                Category = t.Category,
                AuthorName = AuthorDisplayName(t.Author),
                AuthorInitials = Initials(AuthorDisplayName(t.Author)),
                ApprovedByNutritionist = t.ApprovedByNutritionist,
                CreatedAt = t.CreatedAt,
                CanDelete = currentUserId.HasValue && t.AuthorId == currentUserId.Value,
                Comments = t.Comments
                    .OrderBy(c => c.CreatedAt)
                    .Select(c => new TipCommentViewModel
                    {
                        AuthorName = AuthorDisplayName(c.Author),
                        AuthorInitials = Initials(AuthorDisplayName(c.Author)),
                        Body = c.Body,
                        CreatedAt = c.CreatedAt
                    }).ToList()
            }).ToList()
        };
    }

    public async Task<int> CreateAsync(int authorId, CreateTipInputModel input)
    {
        var author = await _userRepository.GetByIdAsync(authorId);

        var tip = new Tip
        {
            Body = input.Body.Trim(),
            Category = input.Category,
            AuthorId = authorId,
            ApprovedByNutritionist = author is Nutritionist
        };

        await _tipRepository.AddAsync(tip);
        await _unitOfWork.SaveChangesAsync();
        return tip.Id;
    }

    public async Task<bool> AddCommentAsync(int authorId, CreateTipCommentInputModel input)
    {
        var tip = await _tipRepository.GetByIdAsync(input.TipId);
        if (tip is null) return false;

        tip.Comments.Add(new TipComment
        {
            TipId = tip.Id,
            AuthorId = authorId,
            Body = input.Body.Trim()
        });

        _tipRepository.Update(tip);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id, int authorId)
    {
        var tip = await _tipRepository.GetWithCommentsAsync(id);
        if (tip is null || tip.AuthorId != authorId) return false;

        _tipRepository.Remove(tip);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    private static string AuthorDisplayName(User user) =>
        user is Nutritionist n && !string.IsNullOrWhiteSpace(n.DisplayName) ? n.DisplayName : user.FullName;

    private static string Initials(string name) =>
        string.IsNullOrWhiteSpace(name)
            ? "?"
            : string.Concat(name.Split(' ', StringSplitOptions.RemoveEmptyEntries).Take(2).Select(w => char.ToUpper(w[0])));
}

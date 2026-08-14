using NutriConect.Business.Enums;

namespace NutriConect.Business.ViewModels;

public class TipCardViewModel
{
    public int Id { get; set; }
    public string Body { get; set; } = string.Empty;
    public TipCategoryEnum? Category { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorInitials { get; set; } = string.Empty;
    public bool ApprovedByNutritionist { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public bool CanDelete { get; set; }
    public List<TipCommentViewModel> Comments { get; set; } = [];
}

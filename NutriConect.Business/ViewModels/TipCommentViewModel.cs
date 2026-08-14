namespace NutriConect.Business.ViewModels;

public class TipCommentViewModel
{
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorInitials { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
}

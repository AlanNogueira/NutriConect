namespace NutriConect.Business.ViewModels;

public class RecipeReviewViewModel
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public bool IsAnonymous { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorInitials { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public bool CanDelete { get; set; }
}

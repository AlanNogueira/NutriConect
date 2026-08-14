namespace NutriConect.Business.Entities;

public class RecipeReview
{
    public int Id { get; set; }

    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;

    public int AuthorId { get; set; }
    public User Author { get; set; } = null!;

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public bool IsAnonymous { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}

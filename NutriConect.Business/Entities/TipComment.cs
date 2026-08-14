namespace NutriConect.Business.Entities;

public class TipComment
{
    public int Id { get; set; }
    public int TipId { get; set; }
    public Tip Tip { get; set; } = null!;
    public string Body { get; set; } = string.Empty;

    public int AuthorId { get; set; }
    public User Author { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}

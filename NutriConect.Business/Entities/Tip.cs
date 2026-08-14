using NutriConect.Business.Enums;

namespace NutriConect.Business.Entities;

public class Tip
{
    public int Id { get; set; }
    public string Body { get; set; } = string.Empty;

    public TipCategoryEnum? Category { get; set; }

    public int AuthorId { get; set; }
    public User Author { get; set; } = null!;

    public bool ApprovedByNutritionist { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public ICollection<TipComment> Comments { get; set; } = [];
}

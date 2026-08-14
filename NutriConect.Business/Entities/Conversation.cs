namespace NutriConect.Business.Entities;

public class Conversation
{
    public int Id { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public int NutritionistId { get; set; }
    public Nutritionist Nutritionist { get; set; } = null!;

    public List<ChatMessage> Messages { get; set; } = [];

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset LastMessageAt { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? ClientDeletedAt { get; set; }
    public DateTimeOffset? NutritionistDeletedAt { get; set; }
}

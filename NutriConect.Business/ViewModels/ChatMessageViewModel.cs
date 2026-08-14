namespace NutriConect.Business.ViewModels;

public class ChatMessageViewModel
{
    public int Id { get; set; }
    public int ConversationId { get; set; }
    public int SenderId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTimeOffset SentAt { get; set; }
}

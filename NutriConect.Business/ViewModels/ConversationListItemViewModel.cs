namespace NutriConect.Business.ViewModels;

public class ConversationListItemViewModel
{
    public int ConversationId { get; set; }
    public string OtherName { get; set; } = string.Empty;
    public string OtherInitials { get; set; } = string.Empty;
    public string? LastMessagePreview { get; set; }
    public DateTimeOffset LastMessageAt { get; set; }
    public int UnreadCount { get; set; }
}

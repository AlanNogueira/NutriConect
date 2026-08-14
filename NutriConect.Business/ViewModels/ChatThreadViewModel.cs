namespace NutriConect.Business.ViewModels;

public class ChatThreadViewModel
{
    public int ConversationId { get; set; }
    public string OtherName { get; set; } = string.Empty;
    public string OtherInitials { get; set; } = string.Empty;
    public List<ChatMessageViewModel> Messages { get; set; } = [];
}

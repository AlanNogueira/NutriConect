namespace NutriConect.Business.ViewModels;

public class ChatMessageSentViewModel
{
    public ChatMessageViewModel Message { get; set; } = null!;
    public int RecipientId { get; set; }
}

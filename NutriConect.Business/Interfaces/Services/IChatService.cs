using NutriConect.Business.InputModels;
using NutriConect.Business.ViewModels;

namespace NutriConect.Business.Interfaces.Services;

public interface IChatService
{
    Task<int?> StartConversationAsync(int clientId, int nutritionistId);
    Task<IEnumerable<ConversationListItemViewModel>> GetConversationsAsync(int userId);
    Task<ChatThreadViewModel?> GetThreadAsync(int conversationId, int userId);
    Task<ChatMessageSentViewModel?> SendMessageAsync(int senderId, SendChatMessageInputModel input);
    Task<bool> DeleteForUserAsync(int conversationId, int userId);
    Task<int> GetUnreadCountAsync(int userId);
    Task<bool> IsParticipantAsync(int conversationId, int userId);
}

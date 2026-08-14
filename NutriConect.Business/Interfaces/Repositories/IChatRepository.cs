using NutriConect.Business.Entities;

namespace NutriConect.Business.Interfaces.Repositories;

public interface IChatRepository : IRepository<Conversation>
{
    Task<Conversation?> GetByPairAsync(int clientId, int nutritionistId);
    Task<Conversation?> GetWithMessagesAsync(int conversationId);
    Task<IEnumerable<Conversation>> GetConversationsForUserAsync(int userId);
    Task<int> CountUnreadForUserAsync(int userId);
}

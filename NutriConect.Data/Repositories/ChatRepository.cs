using Microsoft.EntityFrameworkCore;
using NutriConect.Business.Entities;
using NutriConect.Business.Interfaces.Repositories;
using NutriConect.Data.Context;

namespace NutriConect.Data.Repositories;

public class ChatRepository : Repository<Conversation>, IChatRepository
{
    public ChatRepository(AppDbContext context) : base(context) { }

    public async Task<Conversation?> GetByPairAsync(int clientId, int nutritionistId) =>
        await _context.Set<Conversation>()
            .FirstOrDefaultAsync(c => c.ClientId == clientId && c.NutritionistId == nutritionistId);

    public async Task<Conversation?> GetWithMessagesAsync(int conversationId) =>
        await _context.Set<Conversation>()
            .Include(c => c.Client)
            .Include(c => c.Nutritionist)
            .Include(c => c.Messages.OrderBy(m => m.SentAt))
            .FirstOrDefaultAsync(c => c.Id == conversationId);

    public async Task<IEnumerable<Conversation>> GetConversationsForUserAsync(int userId) =>
        await _context.Set<Conversation>()
            .Include(c => c.Client)
            .Include(c => c.Nutritionist)
            .Include(c => c.Messages)
            .Where(c =>
                (c.ClientId == userId
                    && (c.ClientDeletedAt == null || c.Messages.Any(m => m.SentAt > c.ClientDeletedAt)))
                || (c.NutritionistId == userId
                    && (c.NutritionistDeletedAt == null || c.Messages.Any(m => m.SentAt > c.NutritionistDeletedAt))))
            .OrderByDescending(c => c.LastMessageAt)
            .ToListAsync();

    public async Task<int> CountUnreadForUserAsync(int userId) =>
        await _context.Set<ChatMessage>()
            .CountAsync(m => !m.IsRead
                && m.SenderId != userId
                && ((m.Conversation.ClientId == userId
                        && (m.Conversation.ClientDeletedAt == null || m.SentAt > m.Conversation.ClientDeletedAt))
                    || (m.Conversation.NutritionistId == userId
                        && (m.Conversation.NutritionistDeletedAt == null || m.SentAt > m.Conversation.NutritionistDeletedAt))));
}

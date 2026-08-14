using NutriConect.Business.Entities;
using NutriConect.Business.InputModels;
using NutriConect.Business.Interfaces;
using NutriConect.Business.Interfaces.Repositories;
using NutriConect.Business.Interfaces.Services;
using NutriConect.Business.ViewModels;

namespace NutriConect.Business.Services;

public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChatService(
        IChatRepository chatRepository,
        IRepository<User> userRepository,
        IUnitOfWork unitOfWork)
    {
        _chatRepository = chatRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int?> StartConversationAsync(int clientId, int nutritionistId)
    {
        var existing = await _chatRepository.GetByPairAsync(clientId, nutritionistId);
        if (existing is not null) return existing.Id;

        if (await _userRepository.GetByIdAsync(clientId) is not Client) return null;
        if (await _userRepository.GetByIdAsync(nutritionistId) is not Nutritionist) return null;

        var conversation = new Conversation
        {
            ClientId = clientId,
            NutritionistId = nutritionistId
        };

        await _chatRepository.AddAsync(conversation);
        await _unitOfWork.SaveChangesAsync();
        return conversation.Id;
    }

    public async Task<IEnumerable<ConversationListItemViewModel>> GetConversationsAsync(int userId)
    {
        var conversations = await _chatRepository.GetConversationsForUserAsync(userId);

        return conversations.Select(c =>
        {
            var otherName = OtherParticipantName(c, userId);
            var visible = VisibleMessages(c, userId).ToList();
            var lastMessage = visible.OrderByDescending(m => m.SentAt).FirstOrDefault();

            return new ConversationListItemViewModel
            {
                ConversationId = c.Id,
                OtherName = otherName,
                OtherInitials = Initials(otherName),
                LastMessagePreview = lastMessage?.Content,
                LastMessageAt = lastMessage?.SentAt ?? c.CreatedAt,
                UnreadCount = visible.Count(m => !m.IsRead && m.SenderId != userId)
            };
        }).ToList();
    }

    public async Task<ChatThreadViewModel?> GetThreadAsync(int conversationId, int userId)
    {
        var conversation = await _chatRepository.GetWithMessagesAsync(conversationId);
        if (conversation is null || !IsParticipant(conversation, userId)) return null;

        var unread = conversation.Messages.Where(m => !m.IsRead && m.SenderId != userId).ToList();
        if (unread.Count > 0)
        {
            unread.ForEach(m => m.IsRead = true);
            await _unitOfWork.SaveChangesAsync();
        }

        var otherName = OtherParticipantName(conversation, userId);

        return new ChatThreadViewModel
        {
            ConversationId = conversation.Id,
            OtherName = otherName,
            OtherInitials = Initials(otherName),
            Messages = VisibleMessages(conversation, userId)
                .OrderBy(m => m.SentAt)
                .Select(ToViewModel)
                .ToList()
        };
    }

    public async Task<ChatMessageSentViewModel?> SendMessageAsync(int senderId, SendChatMessageInputModel input)
    {
        var content = input.Content.Trim();
        if (content.Length is 0 or > 2000) return null;

        var conversation = await _chatRepository.GetByIdAsync(input.ConversationId);
        if (conversation is null || !IsParticipant(conversation, senderId)) return null;

        var message = new ChatMessage
        {
            ConversationId = conversation.Id,
            SenderId = senderId,
            Content = content
        };

        conversation.Messages.Add(message);
        conversation.LastMessageAt = message.SentAt;
        _chatRepository.Update(conversation);
        await _unitOfWork.SaveChangesAsync();

        return new ChatMessageSentViewModel
        {
            Message = ToViewModel(message),
            RecipientId = conversation.ClientId == senderId ? conversation.NutritionistId : conversation.ClientId
        };
    }

    public async Task<bool> DeleteForUserAsync(int conversationId, int userId)
    {
        var conversation = await _chatRepository.GetByIdAsync(conversationId);
        if (conversation is null || !IsParticipant(conversation, userId)) return false;

        if (conversation.ClientId == userId)
            conversation.ClientDeletedAt = DateTimeOffset.UtcNow;
        else
            conversation.NutritionistDeletedAt = DateTimeOffset.UtcNow;

        _chatRepository.Update(conversation);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetUnreadCountAsync(int userId) =>
        await _chatRepository.CountUnreadForUserAsync(userId);

    public async Task<bool> IsParticipantAsync(int conversationId, int userId)
    {
        var conversation = await _chatRepository.GetByIdAsync(conversationId);
        return conversation is not null && IsParticipant(conversation, userId);
    }

    private static IEnumerable<ChatMessage> VisibleMessages(Conversation conversation, int userId)
    {
        var cutoff = conversation.ClientId == userId
            ? conversation.ClientDeletedAt
            : conversation.NutritionistDeletedAt;
        return cutoff is null
            ? conversation.Messages
            : conversation.Messages.Where(m => m.SentAt > cutoff);
    }

    private static bool IsParticipant(Conversation conversation, int userId) =>
        conversation.ClientId == userId || conversation.NutritionistId == userId;

    private static ChatMessageViewModel ToViewModel(ChatMessage message) => new()
    {
        Id = message.Id,
        ConversationId = message.ConversationId,
        SenderId = message.SenderId,
        Content = message.Content,
        SentAt = message.SentAt
    };

    private static string OtherParticipantName(Conversation conversation, int userId) =>
        conversation.ClientId == userId
            ? DisplayName(conversation.Nutritionist)
            : conversation.Client.FullName;

    private static string DisplayName(Nutritionist nutritionist) =>
        !string.IsNullOrWhiteSpace(nutritionist.DisplayName) ? nutritionist.DisplayName : nutritionist.FullName;

    private static string Initials(string name) =>
        string.IsNullOrWhiteSpace(name)
            ? "?"
            : string.Concat(name.Split(' ', StringSplitOptions.RemoveEmptyEntries).Take(2).Select(w => char.ToUpper(w[0])));
}

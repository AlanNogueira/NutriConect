using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NutriConect.Business.InputModels;
using NutriConect.Business.Interfaces.Services;

namespace NutriConect.Hubs;

[Authorize]
public class ChatHub : Hub
{
    private readonly IChatService _chatService;

    public ChatHub(IChatService chatService) => _chatService = chatService;

    private int CurrentUserId =>
        int.Parse(Context.User!.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public static string UserGroup(int userId) => $"user-{userId}";
    public static string ConversationGroup(int conversationId) => $"conversation-{conversationId}";

    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, UserGroup(CurrentUserId));
        await base.OnConnectedAsync();
    }

    public async Task JoinConversation(int conversationId)
    {
        if (!await _chatService.IsParticipantAsync(conversationId, CurrentUserId)) return;
        await Groups.AddToGroupAsync(Context.ConnectionId, ConversationGroup(conversationId));
    }

    public async Task LeaveConversation(int conversationId) =>
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, ConversationGroup(conversationId));

    public async Task SendMessage(int conversationId, string content)
    {
        var input = new SendChatMessageInputModel { ConversationId = conversationId, Content = content ?? string.Empty };
        var result = await _chatService.SendMessageAsync(CurrentUserId, input);
        if (result is null) return;

        await Clients.Group(ConversationGroup(conversationId)).SendAsync("ReceiveMessage", result.Message);

        var unread = await _chatService.GetUnreadCountAsync(result.RecipientId);
        await Clients.Group(UserGroup(result.RecipientId)).SendAsync("UnreadChanged", unread);
    }
}

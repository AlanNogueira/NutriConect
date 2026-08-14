using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriConect.Business.Interfaces.Services;

namespace NutriConect.Controllers;

[Authorize]
public class ChatController : Controller
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService) => _chatService = chatService;

    private int CurrentUserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> Conversations() =>
        Json(await _chatService.GetConversationsAsync(CurrentUserId));

    [HttpGet]
    public async Task<IActionResult> Messages(int id)
    {
        var thread = await _chatService.GetThreadAsync(id, CurrentUserId);
        return thread is null ? NotFound() : Json(thread);
    }

    [HttpGet]
    public async Task<IActionResult> UnreadCount() =>
        Json(new { count = await _chatService.GetUnreadCountAsync(CurrentUserId) });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int conversationId)
    {
        var ok = await _chatService.DeleteForUserAsync(conversationId, CurrentUserId);
        return ok ? Ok() : NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Start(int nutritionistId)
    {
        var conversationId = await _chatService.StartConversationAsync(CurrentUserId, nutritionistId);
        return conversationId is null ? NotFound() : Json(new { conversationId });
    }
}

using System.ComponentModel.DataAnnotations;

namespace NutriConect.Business.InputModels;

public class SendChatMessageInputModel
{
    [Required]
    public int ConversationId { get; set; }

    [Required(ErrorMessage = "Digite uma mensagem.")]
    [MaxLength(2000, ErrorMessage = "A mensagem deve ter no máximo 2000 caracteres.")]
    public string Content { get; set; } = string.Empty;
}

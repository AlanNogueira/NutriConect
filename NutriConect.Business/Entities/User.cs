using Microsoft.AspNetCore.Identity;

namespace NutriConect.Business.Entities;

public abstract class User : IdentityUser<int>
{
    public string FullName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? City { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? PhotoUrl { get; set; }
    public bool AcceptedTerms { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}

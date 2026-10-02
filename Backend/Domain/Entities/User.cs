using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    public string DisplayName { get; set; } = null!;
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }
}
namespace Application.Accounts.DTOs;

public class RegisterDto
{
    public required string DisplayName { get; set; } = string.Empty;
    public required string Email { get; set; }
    public required string Password { get; set; }
}

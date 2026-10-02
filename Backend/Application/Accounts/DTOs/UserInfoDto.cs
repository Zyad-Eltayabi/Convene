namespace Application.Accounts.DTOs;

public class UserInfoDto
{
    public required string DisplayName { get; set; }
    public string? Email { get; set; }
    public required string Id { get; set; }
    public string? ImageUrl { get; set; }
}

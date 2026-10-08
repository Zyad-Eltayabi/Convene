namespace Application.Profiles.DTOs;

public class UserProfileDto
{
    public string Id { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string Bio { get; set; } = null!;
    public string? ImageUrl { get; set; } = null!;
}

using Application.Profiles.DTOs;

namespace Application.Activities.DTOs;

public class ActivityDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public bool IsCancelled { get; set; } = false;
    // Location properties
    public string City { get; set; } = string.Empty;
    public string Venue { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string HostDisplayName { get; set; } = string.Empty;
    public string HostId { get; set; } = string.Empty;
    public ICollection<UserProfileDto> Attendees { get; set; } = [];  
}
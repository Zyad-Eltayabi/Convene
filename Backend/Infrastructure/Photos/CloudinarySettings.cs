namespace Infrastructure.Photos;

public class CloudinarySettings
{
    public string CloudName = Environment.GetEnvironmentVariable("CloudName") ?? throw new Exception("Can't get the cloudName variable in this time");
    public string ApiKey = Environment.GetEnvironmentVariable("ApiKey") ?? throw new Exception("Can't get the ApiKey variable in this time");
    public string ApiSecret = Environment.GetEnvironmentVariable("ApiSecret") ?? throw new Exception("Can't get the ApiSecret variable in this time");
}
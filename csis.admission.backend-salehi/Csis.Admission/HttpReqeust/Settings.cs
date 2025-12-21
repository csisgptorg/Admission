namespace Csis.Admission;
public class HttpRequestSettings
{
    public string BaseUrl { get; set; }
    public string ApiKey { get; set; }
    public int TimeoutInSeconds { get; set; } = 30;
}

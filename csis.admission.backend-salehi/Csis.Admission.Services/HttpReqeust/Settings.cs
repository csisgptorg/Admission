namespace Csis.Admission.Services;
public partial class HttpRequestService
{
    public class HttpRequestOptions
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public int TimeoutInSeconds { get; set; } = 30;
    }
}

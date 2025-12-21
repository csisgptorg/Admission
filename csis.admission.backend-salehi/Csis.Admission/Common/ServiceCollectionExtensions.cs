using Csis.Admission;

namespace Microsoft.Extensions.DependencyInjection;
public static class ServiceCollectionExtensions
{
    public static void AddCsisAdmission(this IServiceCollection services, Action<HttpRequestSettings> httpRequestSettings) {

        var throwMessage = nameof(CsisAdmissionService) + nameof(HttpRequestSettings);
        ArgumentNullException.ThrowIfNull(httpRequestSettings,throwMessage);

        services.AddHttpContextAccessor();
        services.Configure(httpRequestSettings);
        services.AddHttpClient<AdmissionHttpRequestService>();
        services.AddScoped<ICsisAdmissionService, CsisAdmissionService>();
    }
}

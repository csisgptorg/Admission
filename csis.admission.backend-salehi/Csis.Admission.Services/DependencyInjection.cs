using Microsoft.Extensions.Configuration;
using Csis.Admission.Services.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Csis.Admission.Application.Common.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Csis.Admission.Services;

/// <summary>
/// Dependency registrar for services layer
/// </summary>
public static partial class DependencyInjection
{
    /// <summary>
    /// Register custom external services here
    /// </summary>
    /// <param name="services"></param>
    private static void AddCustomServices(this IServiceCollection services, IConfiguration configuration) {
        services.AddScoped<ICsisWsmService, CsisWsmService>();
        services.AddScoped<IHttpRequestService, HttpRequestService>();
        services.AddScoped<IRequestService, RequestService>();
        services.AddScoped<ICsisHealthInsuranceService, CsisHealthInsuranceService>();
        services.AddScoped<ICsisSupInsuranceService, CsisSupInsuranceService>();
        services.AddScoped<ICaseFillingRequestService, CaseFillingRequestService>();
        services.AddScoped<IBirthCertService, BirthCertService>();
    }

    /// <summary>
    /// Register background services here
    /// </summary>
    /// <param name="services"></param>
    private static void AddBackgroundServices(this IServiceCollection services, IConfiguration configuration) {
        //services.AddHostedService<RequestCommandBackgroundService>();
    }

    /// <summary>
    /// Register health checks
    /// </summary>
    /// <param name="healthChecks"></param>
    private static void AddHealthChecks(IHealthChecksBuilder healthChecks, IConfiguration configuration) {
        healthChecks
            .AddCheck<StudentDataServiceHealthCheck>(
                name: "Student Data Service",
                failureStatus: HealthStatus.Degraded,
                tags: ["external", "base"],
                timeout: TimeSpan.FromSeconds(3)
            );
    }
}

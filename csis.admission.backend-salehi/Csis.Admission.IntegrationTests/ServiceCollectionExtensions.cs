/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Microsoft.Extensions.DependencyInjection;

namespace Csis.Admission.IntegrationTests;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection Remove<TService>(this IServiceCollection services) {
        var serviceDescriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(TService));

        if ( serviceDescriptor is not null ) {
            services.Remove(serviceDescriptor);
        }

        return services;
    }
}

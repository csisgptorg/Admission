using Csis.Admission.Application.Common.Behaviors;
using Csis.Admission.Application.Common.Services;
using Csis.Admission.Application.Features.CaseFilings.Validator;
using Csis.Admission.Application.Features.Students.Validators;
using Csis.CompareImageAi.Extensions;
using Csis.FileManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Csis.Admission.Application;

/// <summary>
/// Dependency registrar for application layer
/// </summary>
public static partial class DependencyInjection
{
    /// <summary>
    /// Register custom application services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    private static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration) {
        services.AddCsisFileManagement(config => {
            config.BaseUrl = configuration.GetValue<string>("FileManagementServiceOptions:BaseUrl");
            config.ApiKey = configuration.GetValue<string>("FileManagementServiceOptions:ApiKey");
        });

        services.AddCsisCompareImageAiApi();

        services.AddScoped<IOtpSenderService, OtpSenderService>();
        
        // À»  MediatR Pipeline Behaviors
        services.RegisterMediatRBehaviors();
        services.AddScoped<IdentityValidator>();
        services.AddScoped<ApprovalCenterValidator>();
        services.AddScoped<BirthCertValidator>();
    }

    /// <summary>
    /// Register MediatR Pipeline Behaviors
    /// </summary>
    /// <param name="services"></param>
    private static void RegisterMediatRBehaviors(this IServiceCollection services) {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PublicRouteCodmBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DeceasedValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(YektaCodeValidationBehavior<,>));
    }
}

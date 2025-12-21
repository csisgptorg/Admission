using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Enums;
using Csis.Admission.WebApi.Filters;
using Csis.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Csis.Admission.WebApi.Extensions;

/// <summary>
/// Dependency registrar for presentation layer
/// </summary>
public static partial class DependencyInjection
{
    /// <summary>
    /// Register custom presentation services here
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddCustomPresentationServices(this IServiceCollection services, IConfiguration configuration) {

    }

    /// <summary>
    /// Register custom typed options
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddCustomTypedOptions(this IServiceCollection services, IConfiguration configuration) {
        services.Configure<ImageAnalysisOption>(configuration.GetSection("ImageAnalysis"));
    }

    /// <summary>
    /// Register custom third party libraries
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddThirdPartyLibraries(this IServiceCollection services, IConfiguration configuration) {
        services.AddCsisNotificationAdvanced(x => {
            x.BaseUrl = configuration.GetValue<string>("NotificationOptions:BaseUrl");
            x.ApiKey = configuration.GetValue<string>("NotificationOptions:ApiKey");
            x.TimeoutInSeconds = configuration.GetValue<int>("NotificationOptions:TimeoutInSeconds");
        });

        services.AddCsisNotification(x => {
            x.BaseUrl = configuration.GetValue<string>("NotificationOptions:BaseUrl");
            x.ApiKey = configuration.GetValue<string>("NotificationOptions:ApiKey");
            x.TimeoutInSeconds = configuration.GetValue<int>("NotificationOptions:TimeoutInSeconds");
        });
    }

    /// <summary>
    /// Register authorization related services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddAuthorizationServices(this IServiceCollection services, IConfiguration configuration) {
        services.AddCsisAuthorization<PermissionsEnum>(options => {
            options.BaseUrl = configuration.GetValue<string>("IdentityServerOptions:BaseUrl");
            options.ApiKey = configuration.GetValue<string>("IdentityServerOptions:ApiKey");
            options.TimeoutInSeconds = configuration.GetValue<int>("IdentityServerOptions:TimeoutInSeconds");
            options.EnableDeveloperMode = configuration.GetValue<bool>("IdentityServerOptions:EnableDeveloperMode");
        });
    }

    /// <summary>
    /// Add custom exception handlers
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddCustomExceptionHandlers(this IServiceCollection services, IConfiguration configuration) {
        services.AddExceptionHandler<LogExceptionHandler>();
        services.AddExceptionHandler<NeedOtpCommandExceptionHandler>();
        services.AddExceptionHandler<ConfirmedValidationExceptionHandler>();
    }

    /// <summary>
    /// Initialize global options
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void InitializeGlobalOptions(this IServiceCollection services, IConfiguration configuration) {

    }

    /// <summary>
    /// Add custom configs for swagger
    /// </summary>
    /// <param name="swagger"></param>
    public static void CustomizeSwagger(SwaggerGenOptions swagger) {
        swagger.OperationFilter<ApiKeyHeaderOperationFilter>();
    }

    /// <summary>
    /// Customize MVC options
    /// </summary>
    /// <param name="mvcOptions"></param>
    public static void CustomizeMvcOptions(MvcOptions mvcOptions) {

    }

    /// <summary>
    /// Customize JSON options
    /// </summary>
    /// <param name="jsonOptions"></param>
    public static void CustomizeJsonOptions(JsonOptions jsonOptions) {

    }
}

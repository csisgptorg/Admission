/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Behaviors;
using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;

namespace Csis.Admission.Application;

/// <summary>
/// Dependency registrar for application layer
/// </summary>
public static partial class DependencyInjection
{
    private const string AutoMapperMediatRLicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzg2MzIwMDAwIiwiaWF0IjoiMTc1NDgwODE1NSIsImFjY291bnRfaWQiOiIwMTk4OTJiNzJiOTI3NTdlYWQ5ODAyZjFkY2E2NjQwMSIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazI5YmV6aGE3ZW4yYTk5M2prMTI1aHI1Iiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.rJRVTMZ72ITxTiWFBltT0hyrUMTzrJtuqNxTS8bbe1xeqDTdzUvaWVAskag3SHNIH_ZqxMi9uGHPo4Mct5iIAgR4LtQeh8E1MIsmEav7TKsojk_-gLlsGPnfRUJIT0nKJhD_P3JNn5yfwJhBbTW_EnaGJAByH83MIwYkEjuvzLMXoTvGWLdTMTXRJWCH9USqDE1rfe8FkPcz5_NwHH5ePtjRWw-AGLUwWIC_3-k7IsXnize0hsJ2mtMMb3TcZy3MFIA576V-G4z3RrF7Bqs3EwrjrjcZGCoS2suL4HveLYMdCIWhKi4on0ZASkF60rMPROGfq1z5ERoWYls6198-IA";

    /// <summary>
    /// Register application layer dependencies
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration) {
        services.RegisterAutoMapper();
        services.RegisterMediatR();
        services.RegisterFluentValidation();
        services.AddApplicationServices(configuration);
    }

    /// <summary>
    /// Register auto mapper profiles
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies"></param>
    private static void RegisterAutoMapper(this IServiceCollection services, params Assembly[] assemblies) {
        services.AddAutoMapper(config => {
            config.AddCustomMappingProfile();
            config.LicenseKey = AutoMapperMediatRLicenseKey;
        }, assemblies);
    }

    /// <summary>
    /// Register MediatR services
    /// </summary>
    /// <param name="services"></param>
    private static void RegisterMediatR(this IServiceCollection services) {
        services.AddMediatR(opts => {
            opts.RegisterServicesFromAssemblyContaining<IDto>();
            opts.LicenseKey = AutoMapperMediatRLicenseKey;
        });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    }

    /// <summary>
    /// Register FluentValidation validators
    /// </summary>
    /// <param name="services"></param>
    private static void RegisterFluentValidation(this IServiceCollection services) {
        services.AddValidatorsFromAssemblyContaining(typeof(BaseValidator<>));

        ValidatorOptions.Global.LanguageManager.Enabled = true;
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("fa");
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
    }

    /// <summary>
    /// Create mapping profiles dynamically from <see cref="IMappable"/> types
    /// </summary>
    /// <param name="config"></param>
    private static void AddCustomMappingProfile(this IMapperConfigurationExpression config) {
        var assemblies = new Assembly[] { typeof(IMappable).Assembly };

        var allTypes = assemblies.SelectMany(a => a.GetTypes().Where(t => t is { IsClass: true, IsAbstract: false }));

        var mappableTypes = allTypes
            .Where(type => type.GetInterfaces().Contains(typeof(IMappable)))
            .Select(type => (IMappable) Activator.CreateInstance(type));

        config.AddProfile(new AutoMappingProfile(mappableTypes));
        config.AddProfile(new CustomMappingProfile());
    }
}

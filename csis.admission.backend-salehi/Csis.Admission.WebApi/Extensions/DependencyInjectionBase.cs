/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Domain.Common;
using Csis.Admission.WebApi.Controllers;
using Microsoft.OpenApi.Models;

namespace Csis.Admission.WebApi.Extensions;

/// <summary>
/// Dependency registrar for presentation layer
/// </summary>
public static partial class DependencyInjection
{
    #region Options
    /// <summary>
    /// Configures strongly typed options to allow injection of <see cref="Microsoft.Extensions.Options.IOptions{TOptions}"/>
    /// </summary>
    public static void AddTypedOptions(this IServiceCollection services, IConfiguration configuration) {
        services.Configure<ElasticSearchOptions>(configuration.GetSection(nameof(ElasticSearchOptions)));
        services.Configure<RedisOptions>(configuration.GetSection(nameof(RedisOptions)));
        services.Configure<CacheOptions>(configuration.GetSection(nameof(CacheOptions)));
        services.Configure<DatabaseOptions>(configuration.GetSection(nameof(DatabaseOptions)));
        services.Configure<CorsOptions>(configuration.GetSection(nameof(CorsOptions)));
        services.Configure<SwaggerOptions>(configuration.GetSection(nameof(SwaggerOptions)));
        services.Configure<StudentDataServiceOptions>(configuration.GetSection(nameof(StudentDataServiceOptions)));
        services.Configure<EmployeeDataServiceOptions>(configuration.GetSection(nameof(EmployeeDataServiceOptions)));
        services.AddCustomTypedOptions(configuration);
    }
    #endregion

    #region Presentation
    /// <summary>
    /// Add services used or implemented in presentation layer
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddPresentationServices(this IServiceCollection services, IConfiguration configuration) {
        services.AddHttpContextAccessor();
        services.AddCustomPresentationServices(configuration);
    }

    /// <summary>
    /// Adds required controllers and configures api controller behavior
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddApiControllers(this IServiceCollection services, IConfiguration configuration) {
        static void mvcConfigAction(MvcOptions options) {
            options.ReturnHttpNotAcceptable = true;
            options.Filters.Add(new ProducesAttribute("application/json"));

            CustomizeMvcOptions(options);
        }

        (configuration.GetValue<bool>("SupportMvcViews") ?
            services.AddControllersWithViews(mvcConfigAction) : services.AddControllers(mvcConfigAction))
        .ConfigureApiBehaviorOptions(setup => {
            setup.InvalidModelStateResponseFactory = context => {
                var problemDetails = new ValidationProblemDetails(context.ModelState) {
                    Title = "Validation failed.",
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status422UnprocessableEntity
                };

                return new UnprocessableEntityObjectResult(problemDetails) {
                    ContentTypes = { "application/problem+json" }
                };
            };
        })
        .AddJsonOptions(options => {
            options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

            if ( !GlobalOptions.IsDevelopment ) {
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;
                options.AllowInputFormatterExceptionMessages = false;
            }

            CustomizeJsonOptions(options);
        });

        services.AddScoped<FluentValidationActionFilter>();
    }

    /// <summary>
    /// Add swagger and config OpenApi
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options">Swagger options</param>
    public static void AddSwagger(this IServiceCollection services, SwaggerOptions options) {
        services.AddSwaggerGen(setup => {

            setup.CustomSchemaIds(type => type.ToString());

            if ( options.IncludeXmlDocuments ) {
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var xmlDocuments = new string[] {
                    Path.Combine(baseDirectory, $"{typeof(BaseEntity).Assembly.GetName().Name}.xml"),
                    Path.Combine(baseDirectory, $"{typeof(BaseDto<,>).Assembly.GetName().Name}.xml"),
                    Path.Combine(baseDirectory, $"{typeof(ApiControllerBase).Assembly.GetName().Name}.xml")
                };

                foreach ( var xmlDocument in xmlDocuments ) {
                    if ( File.Exists(xmlDocument) ) {
                        setup.IncludeXmlComments(xmlDocument, true);
                    }
                }
            }

            setup.OperationFilter<SwaggerFilterableFields>();
            setup.OperationFilter<SwaggerActionPermission>();
            setup.OperationFilter<SwaggerApiKeyPermission>();
            CustomizeSwagger(setup);

            setup.SwaggerDoc(options.GetVersion(), new OpenApiInfo {
                Version = options.GetVersion(),
                Title = options.DocumentTitle,
                Description = options.Description
            });

            if ( options.AddJwtSupport ) {
                const string scheme = "Bearer";

                setup.AddSecurityDefinition(scheme, new OpenApiSecurityScheme {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = scheme,
                    BearerFormat = "JWT",
                    Description = "##### Input your JWT token **WITHOUT** the Bearer word.",
                });

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = scheme
                            }
                        }, new List<string>()
                    },
                });
            }

        });
    }
    #endregion

    #region Error handling
    /// <summary>
    /// Register exception handlers
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddExceptionHandlers(this IServiceCollection services, IConfiguration configuration) {
        services.AddCustomExceptionHandlers(configuration);
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<BusinessFlowExceptionsHandler>();
        services.AddExceptionHandler<ServiceClientExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
    }
    #endregion
}

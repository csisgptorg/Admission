/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

#region Create WebApplication Builder
using Csis.Admission.Application;
using Csis.Admission.Application.Common;
using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Persistence;
using Csis.Admission.Services;
using Csis.Admission.WebApi.Middleware;
using Csis.DigestAuthentication;
using Csis.DigestAuthentication.Middlewares;
using Csis.DigestAuthentication.Stores.InMemory;
using Csis.Utilities;
using Csis.Utilities.Extensions;
using Csis.Utilities.Middleware;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Exceptions;
using Serilog.Filters;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Developer.json", optional: true);

var services = builder.Services;
var config = builder.Configuration;
if ( config.GetValue<bool>("GlobalOptions:IsDevelopment") ) {
    GlobalOptions.EnableDevelopmentMode();
}

if ( config.GetValue<bool>("GlobalOptions:RunBackgroundServices") ) {
    GlobalOptions.EnableBackgroundServices();
}

if ( config.GetValue<bool>("GlobalOptions:AllowFileUpload") ) {
    GlobalOptions.EnableFileUpload();
}

services.InitializeGlobalOptions(config);
#endregion

#region Configure Logging
builder.Logging.ClearProviders();

var appName = typeof(Program).Assembly.GetName().Name.Replace("WebApi", "").Replace('.', '-').ToLower().Trim('-');
var now = PersianDateTime.Now;
var elasticSearchOptions = config.GetSection(nameof(ElasticSearchOptions)).Get<ElasticSearchOptions>() ?? new();

builder.Host.UseSerilog((host, config) => {
    config
        .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware"))
        .Filter.ByExcluding(Matching.FromSource("LuckyPennySoftware.AutoMapper.License"))
        .Filter.ByExcluding(Matching.FromSource("LuckyPennySoftware.MediatR.License"))
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .WriteTo.Async(wt => wt.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] [TraceId: {TraceId}] {Message:lj}{NewLine}{Exception}[Scope: {Scope}]{NewLine}"), bufferSize: 3000);

    if ( elasticSearchOptions.Enabled ) {
        if ( elasticSearchOptions.Nodes is null || elasticSearchOptions.Nodes.Length == 0 ) {
            throw new Exception("No elastic search node is configured. Please add nodes list in ElasticSearchOptions:Nodes of appsettings.json");
        }

        config
            .WriteTo.Elasticsearch([.. elasticSearchOptions.Nodes.Select(n => new Uri(n))], opts => {
                opts.DataStream = new DataStreamName("logs", $"{appName}-{now.Year:D4}{now.Month:D2}", builder.Environment.EnvironmentName.ToLower());
                opts.BootstrapMethod = BootstrapMethod.Failure;
            });
    }

    config.ReadFrom.Configuration(host.Configuration);

});
#endregion

#region Configure Services
services.AddExceptionHandlers(config);
services.AddTypedOptions(config);
services.AddApplicationLayer(config);
services.AddServicesLayer(config);
services.AddPresentationServices(config);
services.AddAuthorizationServices(config);
services.AddApiControllers(config);
services.AddThirdPartyLibraries(config);
services.AddPersistenceAsync(config);

var swaggerOptions = config.GetSection(nameof(SwaggerOptions)).Get<SwaggerOptions>() ?? new();
var swaggerRouteGroupId = 1;
var healthCheckRouteGroupId = 2;

if ( swaggerOptions.Enabled ) {
    services.AddEndpointsApiExplorer();
    services.AddSwagger(swaggerOptions);
}

var digestAuthenticationOptions = config.GetSection(nameof(DigestAuthenticationOptions)).Get<DigestAuthenticationOptions>() ?? new();
var digestAuthenticationBuilder = services.AddDigestAuthentication(digestAuthenticationOptions.Realm)
    .AddInMemoryKeyStore()
    .AddInMemoryUserStore();

if ( digestAuthenticationOptions.Users.Length > 0 ) {
    foreach ( var user in digestAuthenticationOptions.Users ) {
        if ( user.Username.HasValue() && user.Password.HasValue() && user.Role.HasValue() ) {
            if ( user.Role.Equals("swagger") ) {
                digestAuthenticationBuilder.CreateInMemoryUser(swaggerRouteGroupId, user.Username, user.Password);
            } else if ( user.Role.Equals("health") ) {
                digestAuthenticationBuilder.CreateInMemoryUser(healthCheckRouteGroupId, user.Username, user.Password);
            }
        }
    }
}
#endregion

#region Cross-Origin Resource Sharing (CORS)
var corsOptions = config.GetSection(nameof(CorsOptions)).Get<CorsOptions>() ?? new();

if ( corsOptions.Enabled ) {
    services.AddCors(setup => {
        if ( corsOptions.Origins.Contains("*") ) {
            setup.AddPolicy(CorsOptions.PolicyName, policy =>
                policy
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .WithMethods(corsOptions.Methods));
        } else {
            setup.AddPolicy(CorsOptions.PolicyName, policy =>
                policy
                    .AllowAnyHeader()
                    .WithOrigins(corsOptions.Origins)
                    .WithMethods(corsOptions.Methods));
        }
    });
}
#endregion

#region Configure HTTP Request Pipeline
var app = builder.Build();

MapperProvider.Initialize(app.Services.GetRequiredService<AutoMapper.IMapper>());
var dbOptions = config.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>();
if ( dbOptions.RunSeeders ) {
    var seedersFinished = false;
    app.Use(async (context, requestDelegate) => {
        if ( context is not null && !context.Request.Path.Value.Contains("swagger") ) {
            if ( !seedersFinished ) {
                await app.Services.RunDataSeedersAsync(config);
                seedersFinished = true;
            }
        }

        await requestDelegate(context);
    });
}

app.MiddlewareAfterAppBuild();

app.UseUserIdLogScope();
app.UseIpAddressLogScope();
app.UseUserAgentLogScope();

app.UseExceptionHandler(config => { });
app.UseHttpsRedirection();

if ( swaggerOptions.Enabled ) {
    app.UseDigestAuthentication(swaggerRouteGroupId, [
        $"{swaggerOptions.RoutePrefix}/index.html",
        $"{swaggerOptions.RoutePrefix}/{swaggerOptions.GetVersion()}"
    ]);

    var assetsPrefix = swaggerOptions.AssetsPrefix.HasValue() ?
        $"/{swaggerOptions.AssetsPrefix.Trim().Trim('/').Trim()}/" :
        "/";

    app.MapStaticAssets();
    app.UseSwagger(x => x.RouteTemplate = $"/{swaggerOptions.RoutePrefix}/{{documentName}}/swagger.json");
    app.UseSwaggerUI(opts => {
        opts.InjectStylesheet($"{assetsPrefix}swagger-files/theme.css");
        opts.InjectJavascript($"{assetsPrefix}swagger-files/toast.js");
        opts.InjectJavascript($"{assetsPrefix}swagger-files/toggle-sections.js");
        opts.InjectJavascript($"{assetsPrefix}swagger-files/theme-toggler.js");
        opts.InjectJavascript($"{assetsPrefix}swagger-files/auto-bearer-set.js");

        opts.SwaggerEndpoint($"{assetsPrefix}{swaggerOptions.RoutePrefix}/{swaggerOptions.GetVersion()}/swagger.json",
            $"{swaggerOptions.DocumentTitle} - {swaggerOptions.GetVersion()}");
        opts.RoutePrefix = swaggerOptions.RoutePrefix;
        opts.DocumentTitle = swaggerOptions.DocumentTitle;

        opts.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        opts.EnableTryItOutByDefault();

        if ( swaggerOptions.PersistAuthorization ) {
            opts.EnablePersistAuthorization();
        }
    });
}

if ( corsOptions.Enabled ) {
    app.UseCors(CorsOptions.PolicyName);
}

if ( config.GetValue<bool>("Serilog:EnableRequestLogging") ) {
    app.UseSerilogRequestLogging();
}

app.MiddlewareBeforeAuthentication();

app.UseFakeXPoweredByHeader(XPoweredByValue.JavaServlet6);
app.UseFakeServerHeader(ServerValue.Nginx);

app.UseAuthentication();
app.UseAuthorization();

app.MiddlewareBeforeMapControllers();
app.MapControllers();
app.UseDigestAuthentication(healthCheckRouteGroupId, ["/_health"]);
app.MapHealthChecks("/_health", new HealthCheckOptions {
    AllowCachingResponses = false,
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MiddlewareAfterMapControllers();

await app.RunAsync();
#endregion

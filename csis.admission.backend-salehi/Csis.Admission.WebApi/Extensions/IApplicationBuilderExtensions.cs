using Csis.Admission.WebApi.Middleware;

namespace Csis.Admission.WebApi.Extensions;

/// <summary>
/// Extension methods to register custom middleware
/// </summary>
public static class IApplicationBuilderExtensions
{
    /// <summary>
    /// Register custom middleware that should run before authentication here
    /// </summary>
    /// <param name="app"></param>
    public static void MiddlewareBeforeAuthentication(this WebApplication app) {
    }

    /// <summary>
    /// Register custom middleware that should run after authentication and before map controllers here
    /// </summary>
    /// <param name="app"></param>
    public static void MiddlewareBeforeMapControllers(this WebApplication app) {
        
    }

    /// <summary>
    /// Register custom middleware that should run after map controllers here
    /// </summary>
    /// <param name="app"></param>
    public static void MiddlewareAfterMapControllers(this WebApplication app) {
        
    }

    /// <summary>
    /// Register custom middleware right after application was built
    /// </summary>
    /// <param name="app"></param>
    public static void MiddlewareAfterAppBuild(this WebApplication app) {
        app.UseMiddleware<LogRequestMiddleware>();
    }
}

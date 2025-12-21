/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Authorization.Services;

namespace Csis.Admission.WebApi.Middleware;

/// <summary>
/// Add client user id and username to log scopes
/// </summary>
/// <param name="next"></param>
/// <param name="logger"></param>
public sealed class UserIdLogScopeMiddleware(RequestDelegate next, ILogger<UserIdLogScopeMiddleware> logger)
{
    /// <summary>
    /// Invoke middleware
    /// </summary>
    /// <param name="context"></param>    
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context) {
        var authenticatedUserService = context.RequestServices.GetRequiredService<ICsisAuthenticatedUserService>();
        var userId = await authenticatedUserService.GetUserIdAsync(throwExceptionIfFailed: false);
        var username = userId.HasValue ? await authenticatedUserService.GetUsernameAsync() : null;
        using ( logger.BeginScope("UserId:{userId} Username:{username}", userId, username) ) {
            await next(context);
        }
    }
}

/// <summary>
/// Extension Methods
/// </summary>
public static class UserIdLogScopeMiddlewareExtensions
{
    /// <summary>
    /// Include client user id and username to log scopes
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseUserIdLogScope(this IApplicationBuilder builder) {
        return builder.UseMiddleware<UserIdLogScopeMiddleware>();
    }
}

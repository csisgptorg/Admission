/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Dtos;
using System.Diagnostics;

namespace Csis.Admission.Application.Common.Behaviors;

/// <summary>
/// Behavior to log requests and responses in the pipeline
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
/// <param name="logger"></param>
public sealed class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="next"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken) {
        var type = typeof(TRequest);
        var suppressLogging = !typeof(ILogRequest).IsAssignableFrom(type);

        if ( suppressLogging ) {
            return await next(cancellationToken);
        }

        var sw = Stopwatch.StartNew();
        var requestName = type.Name;

        logger.LogInformation("Executing '{requestName}'. Request: {@request}", requestName, request);

        try {
            var response = await next(cancellationToken);

            sw.Stop();

            var level = sw.ElapsedMilliseconds > 1000 ? LogLevel.Warning : LogLevel.Information;
            logger.Log(level,
                "Handled '{requestName}' in {elapsed} ms. Result: {@response}",
                requestName, sw.ElapsedMilliseconds, response);

            return response;
        } catch ( CommandValidationException ex ) {
            sw.Stop();

            logger.LogInformation(ex, "Command validation exception in '{requestName}' after {elapsed} ms.",
                requestName, sw.ElapsedMilliseconds);

            throw;

        } catch {
            sw.Stop();
            throw;
        }
    }
}

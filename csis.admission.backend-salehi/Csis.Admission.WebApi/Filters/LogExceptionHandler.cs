using System.Diagnostics;
using Csis.Admission.Persistence;
using Csis.Admission.Domain.Entities;
using Microsoft.AspNetCore.Diagnostics;

namespace Csis.Admission.WebApi.Filters;

/// <inheritdoc/>
internal sealed class LogExceptionHandler : IExceptionHandler
{
    private readonly IServiceProvider _serviceProvider;
    public LogExceptionHandler(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {
        
        await using var scope = _serviceProvider.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var traceId = Activity.Current?.TraceId.ToString() ?? httpContext?.TraceIdentifier;
        var url = $"({httpContext.Request.Method}) {httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.Path}";

        var log = new TraceLog(traceId, url, exception.ToString(), "ResponseException");
        db.Set<TraceLog>().Add(log);
        await db.SaveChangesAsync(cancellationToken);

        return false;
    }
}

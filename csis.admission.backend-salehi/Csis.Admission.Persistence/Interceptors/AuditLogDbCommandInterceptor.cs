using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace Csis.Admission.Persistence.Interceptors;

/// <summary>
/// Interceptor to create audit logs when bulk operations executed
/// </summary>
internal sealed class AuditLogDbCommandInterceptor : DbCommandInterceptor
{
    public override async ValueTask<int> NonQueryExecutedAsync(DbCommand command, CommandExecutedEventData eventData, int result, CancellationToken cancellationToken = default) {
        if ( eventData.Context.ChangeTracker is not null &&
            command.CommandText.StartsWith("MERGE", StringComparison.OrdinalIgnoreCase) ) {
            //await eventData.Context.SaveChangesAsync(cancellationToken);
        }

        return await base.NonQueryExecutedAsync(command, eventData, result, cancellationToken);
    }

    public override int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result) {
        if ( eventData.Context.ChangeTracker is not null &&
            command.CommandText.StartsWith("MERGE", StringComparison.OrdinalIgnoreCase) ) {
            //eventData.Context.SaveChanges();
        }

        return base.NonQueryExecuted(command, eventData, result);
    }
}

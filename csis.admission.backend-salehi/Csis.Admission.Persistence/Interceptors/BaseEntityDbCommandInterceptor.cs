/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Persistence.Extensions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace Csis.Admission.Persistence.Interceptors;

/// <summary>
/// Interceptor to set base entity properties like CreatedOn and CreatedById on insert or update
/// This interceptor is only invoked on bulk operations
/// </summary>
internal sealed class BaseEntityDbCommandInterceptor : DbCommandInterceptor
{
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;

    public BaseEntityDbCommandInterceptor(IDateTimeService dateTimeService, ICurrentUserService currentUserService) {
        _dateTimeService = dateTimeService;
        _currentUserService = currentUserService;
    }

    public override async ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default) {
        if ( !command.CommandText.StartsWith("MERGE", StringComparison.OrdinalIgnoreCase) &&
                !command.CommandText.StartsWith("IF ", StringComparison.OrdinalIgnoreCase) ) {
            var userId = await _currentUserService.GetUserIdAsync();
            var delegatedUserId = await _currentUserService.GetDelegatedUserIdAsync();
            var currentDate = _dateTimeService.Now;

            eventData.Context.ChangeTracker.SetBaseEntityProperties(userId, delegatedUserId, currentDate);
        }

        return await base.NonQueryExecutingAsync(command, eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result) {
        if ( !command.CommandText.StartsWith("MERGE", StringComparison.OrdinalIgnoreCase) &&
                !command.CommandText.StartsWith("IF ", StringComparison.OrdinalIgnoreCase) ) {
            var userId = _currentUserService.GetUserIdAsync().Result;
            var delegatedUserId = _currentUserService.GetDelegatedUserIdAsync().Result;
            var currentDate = _dateTimeService.Now;

            eventData.Context.ChangeTracker.SetBaseEntityProperties(userId, delegatedUserId, currentDate);
        }

        return base.NonQueryExecuting(command, eventData, result);
    }
}

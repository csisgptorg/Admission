/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Csis.Admission.Persistence.Interceptors;

/// <summary>
/// Interceptor to set base entity properties like CreatedOn and CreatedById on insert or update
/// This interceptor is not invoked for bulk operations
/// </summary>
internal sealed class BaseEntitySaveChangesInterceptor(IDateTimeService dateTimeService, IServiceProvider serviceProvider) : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default) {
        var currentUserService = serviceProvider.GetRequiredService<ICurrentUserService>();
        var userId = await currentUserService.GetUserIdAsync();
        var delegatedUserId = await currentUserService.GetDelegatedUserIdAsync();
        var currentDate = dateTimeService.Now;

        eventData.Context.ChangeTracker.SetBaseEntityProperties(userId, delegatedUserId, currentDate);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result) {
        var currentUserService = serviceProvider.GetRequiredService<ICurrentUserService>();
        var userId = currentUserService.GetUserIdAsync().Result;
        var delegatedUserId = currentUserService.GetDelegatedUserIdAsync().Result;
        var currentDate = dateTimeService.Now;

        eventData.Context.ChangeTracker.SetBaseEntityProperties(userId, delegatedUserId, currentDate);
        return base.SavingChanges(eventData, result);
    }
}

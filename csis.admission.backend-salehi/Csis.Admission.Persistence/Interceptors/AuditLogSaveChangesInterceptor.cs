using Csis.Authorization;
using Microsoft.AspNetCore.Http;
using Csis.Authorization.Services;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Persistence.Extensions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Csis.Admission.Application.Common.Interfaces;

namespace Csis.Admission.Persistence.Interceptors;

//TODO نیاز به بررسی بیشتر و پیاده سازی به صورت ترنز اکشن دارد
/// <summary></summary>
internal sealed class AuditLogSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IDateTimeService _dateTimeService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ICurrentUserService _currentUserService;
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;

    private readonly List<AuditLog> _auditLogs = [];

    public AuditLogSaveChangesInterceptor(IDateTimeService dateTimeService, IHttpContextAccessor contextAccessor, ICurrentUserService currentUserService,
        ICsisAuthenticatedUserService authenticatedUserService) {
        _dateTimeService = dateTimeService;
        _contextAccessor = contextAccessor;
        _currentUserService = currentUserService;
        _authenticatedUserService = authenticatedUserService;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default) {

        if ( eventData.Context == null ) {
            return result;
        }

        var userId = await _currentUserService.GetUserIdAsync();
        int? codm = int.TryParse(await _authenticatedUserService.GetStudentCodmAsync(), out var studentCodm) ? studentCodm : null;
        var personnelId = await _authenticatedUserService.GetPersonnelIdAsync();
        var delegatedUserId = await _currentUserService.GetDelegatedUserIdAsync();
        var applicationId = _contextAccessor.HttpContext?.GetSourceApplicationId();
        var auditLogs = eventData.Context.ChangeTracker.DetectAuditLogs(personnelId, codm, userId, delegatedUserId, applicationId, _dateTimeService.Now);

        if ( auditLogs.Count > 0 ) {
            _auditLogs.AddRange(auditLogs);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default) {

        if ( _auditLogs.Count > 0 ) {
            eventData.Context.ChangeTracker.SetAuditTableRecordIds(_auditLogs);
            eventData.Context.AddRange(_auditLogs);
        }

        var rowAffected = await base.SavedChangesAsync(eventData, result, cancellationToken);

        if ( _auditLogs.Count > 0 ) {
            _auditLogs.Clear();
            await eventData.Context.SaveChangesAsync(cancellationToken);
        }
        return rowAffected;
    }
}

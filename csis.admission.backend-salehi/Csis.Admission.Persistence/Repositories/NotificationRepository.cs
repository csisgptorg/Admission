/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Persistence.Repositories;
internal sealed partial class NotificationRepository(
    AppDbContext dbContext,
    IMemoryCacheService cache,
    ICacheKeyService<int> cacheKeyService,
    IOptions<CacheOptions> cacheOptions,
    ICurrentUserService currentUserService) : Repository<Domain.Entities.Notification>(dbContext, cache, cacheKeyService, cacheOptions, currentUserService), INotificationRepository
{
    public async Task<List<Domain.Entities.Notification>> GetPendingNotificationsAsync(int maxCount, CancellationToken cancellationToken) {
        return await QueryTracking
            .Where(x => x.Status == NotificationStatus.Pending)
            .OrderByDescending(x => x.Priority)
            .ThenBy(x => x.Id)
            .Take(maxCount)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> HasPendingNotificationAsync(CancellationToken cancellationToken) {
        return await ExistsAsync(x => x.Status == NotificationStatus.Pending, cancellationToken: cancellationToken);
    }
}

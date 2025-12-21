/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Persistence.Repositories;

internal sealed class BackgroundServiceReportRepository(AppDbContext db, IDateTimeService dateTimeService) : IBackgroundServiceReportRepository
{
    private DbSet<BackgroundServiceReport> DbSet => db.Set<BackgroundServiceReport>();

    public async Task FinishAsync(int reportId, string report, long elapsedMilliseconds, CancellationToken cancellationToken) {
        await DbSet
            .Where(x => x.Id == reportId)
            .ExecuteUpdateAsync(p => p
                .SetProperty(e => e.Report, e => report)
                .SetProperty(e => e.Status, e => BackgroundServiceStatus.Finished)
                .SetProperty(e => e.FinishedOn, e => dateTimeService.Now)
                .SetProperty(e => e.ElapsedMilliseconds, e => elapsedMilliseconds), cancellationToken: cancellationToken);
    }

    public async Task FinishWithErrorAsync(int reportId, string error, long elapsedMilliseconds, CancellationToken cancellationToken) {
        await DbSet
            .Where(x => x.Id == reportId)
            .ExecuteUpdateAsync(p => p
                .SetProperty(e => e.Report, e => error)
                .SetProperty(e => e.Status, e => BackgroundServiceStatus.Error)
                .SetProperty(e => e.ElapsedMilliseconds, e => elapsedMilliseconds), cancellationToken: cancellationToken);
    }

    public async Task<DateTime?> GetLastFinishedDateAsync(string serviceTitle, CancellationToken cancellationToken) {
        return await DbSet
            .Where(x => x.ServiceTitle == serviceTitle)
            .OrderByDescending(x => x.Id)
            .Select(x => x.FinishedOn)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> IsRunningAsync(string serviceTitle, CancellationToken cancellationToken) {
        return await DbSet
            .Where(x => x.ServiceTitle == serviceTitle)
            .Where(x => x.Status == BackgroundServiceStatus.Running)
            .AnyAsync(cancellationToken: cancellationToken);
    }

    public async Task<BackgroundServiceReport> StartAsync(string serviceTitle, CancellationToken cancellationToken) {
        var report = new BackgroundServiceReport {
            ServiceTitle = serviceTitle,
            Status = BackgroundServiceStatus.Running
        };
        await DbSet
            .AddAsync(report, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        return report;
    }

    public async Task ForceStopLongRunningServicesAsync(TimeSpan longRunningThreshold, CancellationToken cancellationToken) {
        await DbSet
            .Where(x => x.CreatedOn < dateTimeService.Now.Subtract(longRunningThreshold))
            .Where(x => x.Status == BackgroundServiceStatus.Running)
            .ExecuteUpdateAsync(p => p
                .SetProperty(e => e.Status, e => BackgroundServiceStatus.ForceStopped)
                .SetProperty(e => e.ElapsedMilliseconds, e => -1), cancellationToken: cancellationToken);
    }

    public async Task ForceStopLongRunningServicesAsync(TimeSpan longRunningThreshold, string serviceTitle, CancellationToken cancellationToken) {
        await DbSet
            .Where(x => x.ServiceTitle == serviceTitle)
            .Where(x => x.CreatedOn < dateTimeService.Now.Subtract(longRunningThreshold))
            .Where(x => x.Status == BackgroundServiceStatus.Running)
            .ExecuteUpdateAsync(p => p
                .SetProperty(e => e.Status, e => BackgroundServiceStatus.ForceStopped)
                .SetProperty(e => e.ElapsedMilliseconds, e => -1), cancellationToken: cancellationToken);
    }
}

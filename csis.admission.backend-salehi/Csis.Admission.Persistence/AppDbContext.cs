/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common;
using Csis.Admission.Persistence.Configurations;
using EFCore.BulkExtensions;

namespace Csis.Admission.Persistence;

/// <summary>
/// Application db context
/// </summary>
/// <param name="options"></param>
/// <param name="dateTimeService"></param>
public sealed class AppDbContext(
    DbContextOptions<AppDbContext> options,
    ICurrentUserService currentUserService,
    IDateTimeService dateTimeService) : DbContext(options)
{
    public void BulkSave() {
        ChangeTracker.DetectChanges();
        ChangeTracker.SetBaseEntityProperties(currentUserService.GetUserIdAsync().Result, currentUserService.GetDelegatedUserIdAsync().Result, dateTimeService.Now);
        this.BulkSaveChanges();
    }

    public async Task BulkSaveAsync(CancellationToken cancellationToken = default) {
        ChangeTracker.DetectChanges();
        ChangeTracker.SetBaseEntityProperties(await currentUserService.GetUserIdAsync(), await currentUserService.GetDelegatedUserIdAsync(), dateTimeService.Now);
        await this.BulkSaveChangesAsync(cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Apply model configurations
    /// </summary>
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        builder.RegisterDbSets();
        builder.HasDefaultSchema(Constants.Db.DefaultSchema);
        builder.ApplyConfigurationsFromAssembly(typeof(BaseEntityConfiguration<>).Assembly);

        if ( GlobalOptions.IsDevelopment ) {
            builder.AddXmlComments();
        }
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
    }
}

/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Persistence.Configurations;

internal sealed class BackgroundServiceReportConfiguration : BaseEntityConfiguration<BackgroundServiceReport>
{
    public override void Configure(EntityTypeBuilder<BackgroundServiceReport> builder) {
        base.Configure(builder);

        builder.Property(x => x.ServiceTitle).IsRequired().HasMaxLength(100);
        builder.HasIndex(x => x.ServiceTitle);
    }
}

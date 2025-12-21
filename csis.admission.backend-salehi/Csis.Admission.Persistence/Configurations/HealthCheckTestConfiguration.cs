/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Persistence.Configurations;

internal sealed class HealthCheckTestConfiguration : BaseEntityConfiguration<HealthCheckTest>
{
    public override void Configure(EntityTypeBuilder<HealthCheckTest> builder) {
        base.Configure(builder);

        builder.Property(x => x.CheckText).IsRequired().HasMaxLength(50);
    }
}

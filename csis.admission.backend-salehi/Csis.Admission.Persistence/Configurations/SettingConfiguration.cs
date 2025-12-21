/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Persistence.Configurations;

internal sealed class SettingConfiguration : BaseEntityConfiguration<Setting>
{
    public override void Configure(EntityTypeBuilder<Setting> builder) {
        base.Configure(builder);

        builder.ToTable("Settings", x => {
            x.IsTemporal();
        });

        builder.Property(x => x.Key).IsRequired().HasMaxLength(100);
        builder.HasIndex(x => x.Key).IsUnique();
    }
}

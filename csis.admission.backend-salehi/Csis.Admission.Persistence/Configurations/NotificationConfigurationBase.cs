/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Persistence.Configurations;

internal sealed partial class NotificationConfiguration : BaseEntityConfiguration<Csis.Admission.Domain.Entities.Notification>
{
    public override void Configure(EntityTypeBuilder<Csis.Admission.Domain.Entities.Notification> builder) {
        base.Configure(builder);
        ConfigureCustomFields(builder);

        builder.Property(x => x.Template).HasMaxLength(300);
        builder.Property(x => x.Message).IsRequired().HasMaxLength(500);
        builder.Property(x => x.DeliveryChannels).HasMaxLength(50);
    }
}

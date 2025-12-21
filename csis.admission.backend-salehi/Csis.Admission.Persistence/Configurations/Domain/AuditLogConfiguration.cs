using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;
internal sealed class AuditLogConfiguration : BaseEntityConfiguration<AuditLog>
{
    public override void Configure(EntityTypeBuilder<AuditLog> builder) {
        base.Configure(builder);

        builder.Property(e => e.Time).HasPrecision(3);
        builder.Property(x => x.Table).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Column).IsRequired().HasMaxLength(100);
        builder.Property(x => x.ActionType);
    }
}

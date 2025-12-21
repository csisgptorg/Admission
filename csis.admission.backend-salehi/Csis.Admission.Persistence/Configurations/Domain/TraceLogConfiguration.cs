using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;
internal sealed class TraceLogConfiguration : BaseEntityConfiguration<TraceLog,long>
{
    public override void Configure(EntityTypeBuilder<TraceLog> builder) {
        base.Configure(builder);
        builder.Property(e => e.TraceId).HasMaxLength(100);
        builder.Property(e => e.Url).HasMaxLength(2000);
        builder.Property(e => e.Type).HasMaxLength(100);
    }
}

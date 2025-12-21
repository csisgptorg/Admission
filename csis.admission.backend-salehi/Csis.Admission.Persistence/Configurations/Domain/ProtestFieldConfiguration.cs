using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class ProtestFieldConfiguration : BaseEntityConfiguration<ProtestField,short>
{
    public override void Configure(EntityTypeBuilder<ProtestField> builder) {
        base.Configure(builder);

        builder.ToTable("ProtestFields", "base");
        builder.Property(e => e.Title).HasMaxLength(1000);
    }
}

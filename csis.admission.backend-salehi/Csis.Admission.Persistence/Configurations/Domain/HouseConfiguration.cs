using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class HouseConfiguration : AuditableSoftDeletedEntityConfiguration<House>
{
    public override void Configure(EntityTypeBuilder<House> builder) {
        base.Configure(builder);

        builder.ToTable("tbMaskan");
        builder.Property(x => x.Deleted).HasDefaultValue(false);
    }
}

using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class FamousConfiguration : SoftDeletedBaseEntityConfiguration<Famous>
{
    public override void Configure(EntityTypeBuilder<Famous> builder) {
        base.Configure(builder);
        builder.ToTable("TbMashhoor");

        builder.Property(x => x.Type).HasColumnName("MashhoorType");
        builder.Property(x => x.Role).HasColumnName("Onvan");
        builder.Property(x => x.Area).HasColumnName("MashhoorArea");

        builder.Ignore(x => x.CreatedByDelegatedId);
        builder.Ignore(x => x.Description);
        builder.Ignore(x => x.LastUpdatedByDelegatedId);


    }
}

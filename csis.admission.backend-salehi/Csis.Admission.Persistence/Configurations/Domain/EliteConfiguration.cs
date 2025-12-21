using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;
internal sealed class EliteConfiguration : AuditableSoftDeletedEntityConfiguration<Elite>
{
    public override void Configure(EntityTypeBuilder<Elite> builder) {
        base.Configure(builder);

        builder.ToTable("TbNokhbeInfo");

        builder.Property(x => x.EliteTypeId).HasColumnName("NokhbeType");
        builder.Property(x => x.EliteLevelId).HasColumnName("NokhbeLevel");
        builder.Property(x => x.ApprovalCenterTitle).HasColumnName("MarjaStr");
    }
}

using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class CulturalActivityConfiguration : AuditableSoftDeletedEntityConfiguration<CulturalActivity>
{
    public override void Configure(EntityTypeBuilder<CulturalActivity> builder) {
        base.Configure(builder);

        builder.ToTable("TbFarhangi");
        builder.Property(x => x.Deleted).HasDefaultValue(false);

        builder.Property(x => x.Kind).HasColumnName("KindManage");
        builder.Property(x => x.OtherKind).HasColumnName("CommentManage");
    }
}

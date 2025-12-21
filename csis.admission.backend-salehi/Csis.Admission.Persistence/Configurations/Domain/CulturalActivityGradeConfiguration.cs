using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class CulturalActivityGradeConfiguration : AuditableSoftDeletedEntityConfiguration<CulturalActivityGrade>
{
    public override void Configure(EntityTypeBuilder<CulturalActivityGrade> builder) {
        base.Configure(builder);

        builder.ToTable("TbFarhangiGrade");
        builder.Property(x => x.Deleted).HasDefaultValue(false);

        builder.Property(x => x.ApprovalCenter).HasColumnName("MarkazHozavi");
        builder.Property(x => x.RegisterDate).HasColumnName("DateSabt");
        builder.Property(x => x.ExpirationDate).HasColumnName("DateEtebar");
    }
}

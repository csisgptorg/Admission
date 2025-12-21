using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class TeachGradeConfiguration : AuditableSoftDeletedEntityConfiguration<TeachGrade>
{
    public override void Configure(EntityTypeBuilder<TeachGrade> builder) {
        base.Configure(builder);

        builder.ToTable("TbTadrisGrade");
        builder.Property(x => x.Deleted).HasDefaultValue(false);

        builder.Property(x => x.ApprovalCenter).HasColumnName("MarkazHozavi");
        builder.Property(x => x.RegisterDate).HasColumnName("DateSabt");
        builder.Property(x => x.ExpirationDate).HasColumnName("DateEtebar");
    }
}

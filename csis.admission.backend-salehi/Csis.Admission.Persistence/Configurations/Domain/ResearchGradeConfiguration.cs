using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class ResearchGradeConfiguration : AuditableSoftDeletedEntityConfiguration<ResearchGrade>
{
    public override void Configure(EntityTypeBuilder<ResearchGrade> builder) {
        base.Configure(builder);

        builder.ToTable("TbResearchGrade");
        builder.Property(x =>x.Deleted).HasDefaultValue(false);

        builder.Property(x=>x.ApprovalCenter).HasColumnName("MarkazHozavi");
        builder.Property(x=>x.RegisterDate).HasColumnName("DateSabt");
        builder.Property(x=>x.ExpirationDate).HasColumnName("DateEtebar");
    }
}

using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;
internal sealed class ExcellentConfiguration : AuditableSoftDeletedEntityConfiguration<Excellent>
{
    public override void Configure(EntityTypeBuilder<Excellent> builder) {
        base.Configure(builder);

        builder.ToTable("Momtazin");

        builder.Property(x => x.EducationYearId).HasColumnName("SalMomtaz");
        builder.Property(x => x.EducationLevelId).HasColumnName("maghta");
        builder.Property(x => x.Average).HasColumnName("moadel");

        builder.HasOne(x => x.EducationYear).WithMany().HasForeignKey(x => x.EducationYearId);
    }
}

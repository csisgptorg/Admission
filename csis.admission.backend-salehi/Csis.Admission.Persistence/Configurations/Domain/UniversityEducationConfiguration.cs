using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class UniversityEducationConfiguration : AuditableSoftDeletedEntityConfiguration<UniversityEducation>
{
    public override void Configure(EntityTypeBuilder<UniversityEducation> builder) {
        base.Configure(builder);

        builder.ToTable("TbClassic");

        builder.Property(x=> x.Codm).IsRequired();
        builder.Property(x => x.DependentId).HasColumnName("IDTakaffol");
        builder.Property(x => x.InStudy).HasColumnName("KindTahsil");
        builder.Property(x => x.StudyLevel).HasColumnName("Degree");
        builder.Property(x => x.CourseStudy).HasColumnName("Reshte");
        builder.Property(x => x.UniversityType).HasColumnName("SchoolType");
        builder.Property(x => x.UniversityName).HasColumnName("SchoolName");
        builder.Property(x => x.StartDate).HasColumnName("DataStart");
        builder.Property(x => x.Average).HasColumnName("Moadel");
        builder.Property(x => x.ValidityDate).HasColumnName("TahsilEtebarDate");

        builder.HasOne(x=>x.Dependent).WithMany().HasForeignKey(x => x.DependentId);
    }
}

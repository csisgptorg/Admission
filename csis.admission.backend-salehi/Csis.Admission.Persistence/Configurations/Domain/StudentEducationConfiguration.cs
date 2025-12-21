using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class StudentEducationConfiguration : SoftDeletedBaseEntityConfiguration<StudentEducation>
{
    public override void Configure(EntityTypeBuilder<StudentEducation> builder) {
        base.Configure(builder);

        builder.HasKey(e => e.Id).HasName("PK_Education");

        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.EnteringYear).HasComment("سال ورود به حوزه");
        builder.Property(e => e.StatusInCenter).HasMaxLength(50);

        //builder.HasOne(d => d.StudentCodmNavigation).WithMany(p => p.StudentEducations)
        //    .HasForeignKey(d => d.StudentCodm)
        //    .OnDelete(DeleteBehavior.ClientSetNull)
        //    .HasConstraintName("FK_Education_Student");
    }
}

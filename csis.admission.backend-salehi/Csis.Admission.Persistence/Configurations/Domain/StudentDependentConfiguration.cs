using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;
internal sealed class StudentDependentConfiguration : SoftDeletedBaseEntityConfiguration<StudentDependant>
{
    public override void Configure(EntityTypeBuilder<StudentDependant> builder) {
        base.Configure(builder);

        //builder.HasKey(e => e.Id).HasName("PK_StudentDependent");

        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.CaseCreateDate).HasComment("تاریخ ثبت");
        builder.Property(e => e.CaseDeactiveDate).HasComment("تاریخ انسداد");
        builder.Property(e => e.CaseExpireDate).HasComment("تاریخ بستن پرونده");
        builder.Property(e => e.CaseExpireReason).HasComment("علت بستن پرونده");
        builder.Property(e => e.CaseTransferedTo).HasComment("کد انتقال");
        builder.Property(e => e.Description)
        .HasMaxLength(500)
            .IsUnicode(false);
        builder.Property(e => e.ForceDeactivate).HasComment("صرفا عضو خانواده باید غیر فعال باشد");
        builder.Property(e => e.IsActive).HasComment("وضعیت پرونده");
        builder.Property(e => e.Relationship).HasComment("نسبت ها");
        builder.Property(e => e.RelationshipOrder).HasComment("ترتیب نسبت ها");
        builder.Property(e => e.StatusReason).HasComment("علت");
        builder.Property(e => e.StudentFileCodm).HasComment("کد اصلی");

        //builder.HasOne(d => d.StudentCodmNavigation).WithMany(p => p.StudentDependents)
        //    .HasForeignKey(d => d.StudentCodm)
        //    .OnDelete(DeleteBehavior.ClientSetNull)
        //    .HasConstraintName("FK_StudentDependent_StudentCodm");
    }
}

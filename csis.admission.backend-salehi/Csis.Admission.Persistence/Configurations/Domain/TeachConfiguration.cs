using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class TeachConfiguration : AuditableSoftDeletedEntityConfiguration<Teach>
{
    public override void Configure(EntityTypeBuilder<Teach> builder) {
        base.Configure(builder);

        builder.ToTable("TbTadris");
        builder.Property(x => x.Deleted).HasDefaultValue(false);

        builder.Property(x => x.ProvinceId).HasColumnName("Ostan");
        builder.Property(x => x.CityId).HasColumnName("City");
        builder.Property(x => x.EducationYearId).HasColumnName("SaleTahsili");
        builder.Property(x => x.EducationSemester).HasColumnName("NimSal");
        builder.Property(x => x.EducationLevel).HasColumnName("MaghtaeTadris");
        builder.Property(x => x.Lesson).HasColumnName("DarsTitle");
        builder.Property(x => x.SchoolId).HasColumnName("Madrese");
        builder.Property(x => x.ApprovalCenter).HasColumnName("MarkazeHouzavi");
        builder.Property(x => x.RecordIdInApprovalCenter).HasColumnName("MarakezRecordId");

        builder.HasOne(x => x.Province).WithMany().HasForeignKey(x => x.ProvinceId);
        builder.HasOne(x => x.City).WithMany().HasForeignKey(x => x.CityId);
        builder.HasOne(x => x.EducationYear).WithMany().HasForeignKey(x => x.EducationYearId);
        builder.HasOne(x => x.School).WithMany().HasForeignKey(x => x.SchoolId);
    }
}

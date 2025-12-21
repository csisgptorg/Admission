using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;
internal sealed class PreachConfiguration : AuditableSoftDeletedEntityConfiguration<Preach>
{
    public override void Configure(EntityTypeBuilder<Preach> builder) {
        base.Configure(builder);

        builder.ToTable("TbTabligh");
        builder.Property(x => x.Deleted).HasDefaultValue(false);

        builder.Property(x => x.CountryId).HasColumnName("Country");
        builder.Property(x => x.ProvinceId).HasColumnName("Province");
        builder.Property(x => x.City).HasColumnName("CityTitle");
        builder.Property(x => x.StartDate).HasColumnName("Year");
        builder.Property(x => x.EndDate).HasColumnName("YearTo");
        builder.Property(x => x.ApprovalCenter).HasColumnName("Hokm1");
        builder.Property(x => x.RecordIdInApprovalCenter).HasColumnName("MarakezRecordId");
        builder.Property(x => x.DurationInDays).HasColumnName("DayLong");

        builder.HasOne(x => x.Country).WithMany().HasForeignKey(x => x.CountryId);
        builder.HasOne(x => x.Province).WithMany().HasForeignKey(x => x.ProvinceId);
    }
}

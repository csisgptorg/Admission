using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;
internal sealed class VeteranConfiguration : AuditableSoftDeletedEntityConfiguration<Veteran>
{
    public override void Configure(EntityTypeBuilder<Veteran> builder) {
        base.Configure(builder);

        builder.ToTable("Esargari");

        builder.Property(x => x.HaramDefenceDays).HasColumnName("ModafeHaramTotalDay");
        builder.Property(x => x.HolyDefenseDays).HasColumnName("DefaMoqadasTotalDay");
        builder.Property(x => x.CaptivityDays).HasColumnName("AzadegiTotalDay");
        builder.Property(x => x.JailDays).HasColumnName("ZendanTotalDay");
        builder.Property(x => x.ExileDays).HasColumnName("TabeedTotalDay");
        builder.Property(x => x.VeteranPercent).HasColumnName("JanbaziDarsad");
        builder.Property(x => x.MartyrType).HasColumnName("ShahadatType");
    }
}

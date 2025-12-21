using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class StudentBlockServiceConfiguration : SoftDeletedBaseEntityConfiguration<StudentBlockService>
{
    public override void Configure(EntityTypeBuilder<StudentBlockService> builder) {
        base.Configure(builder);
        builder.ToTable("Emtiaz");
        builder.Property(x => x.ServiceId).HasColumnName("EMtiaz");
        builder.Property(x => x.Reason).HasColumnName("Elat").HasMaxLength(100);
        builder.Property(x => x.BlockDate).HasColumnName("Date");
        builder.HasOne(x => x.Service).WithMany();
    }
}

using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class DependentBlockServiceConfiguration : SoftDeletedBaseEntityConfiguration<DependentBlockService>
{
    public override void Configure(EntityTypeBuilder<DependentBlockService> builder) {
        base.Configure(builder);
        builder.ToTable("EMtiazTakaffol");
        builder.Property(x => x.Codm).HasColumnName("AfradCodm");
        builder.Property(x => x.BlockDate).HasColumnName("Date");
        builder.Property(x => x.ServiceId).HasColumnName("EMtiaz");
        builder.Property(x => x.DependentId).HasColumnName("IdTakaffol");
        builder.Property(x => x.Reason).HasColumnName("Elat");
        builder.HasOne(x => x.Service).WithMany();
    }
}

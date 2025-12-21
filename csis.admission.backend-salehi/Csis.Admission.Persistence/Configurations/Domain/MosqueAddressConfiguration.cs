using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;
internal sealed class MosqueAddressConfiguration : SoftDeletedBaseEntityConfiguration<MosqueAddress>
{
    public override void Configure(EntityTypeBuilder<MosqueAddress> builder) {
        base.Configure(builder);

        //builder.HasOne(a => a.Mosque)
        //     .WithOne(m => m.MosqueAddress)
        //     .HasForeignKey<MosqueAddress>(a => a.MosqueId)
        //      .IsRequired(false)
        //     .OnDelete(DeleteBehavior.Cascade);
    }
}


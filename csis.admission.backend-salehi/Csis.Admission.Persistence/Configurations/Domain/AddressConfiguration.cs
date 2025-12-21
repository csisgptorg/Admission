using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class AddressConfiguration : AuditableSoftDeletedEntityConfiguration<Address> 
{
    public override void Configure(EntityTypeBuilder<Address> builder) {
        base.Configure(builder);

        builder.ToTable("TbAddress");
        builder.Property(x => x.Deleted).HasDefaultValue(false);

        builder.Property(x => x.ProvinceId).HasColumnName("Province");
        builder.Property(x => x.CityId).HasColumnName("City");
        builder.Property(x => x.PortionId).HasColumnName("Portion");
        builder.Property(x => x.TownId).HasColumnName("Town");
        builder.Property(x => x.RuralId).HasColumnName("Rural");
        builder.Property(x => x.Township).HasColumnName("Dorp");
        builder.Property(x => x.ConfirmDate).HasColumnName("ConfirmDate");
        builder.Property(x => x.Flag).HasColumnName("Flg");
    }
}

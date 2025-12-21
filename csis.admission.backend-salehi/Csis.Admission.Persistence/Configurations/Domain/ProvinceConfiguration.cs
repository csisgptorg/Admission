using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class ProvinceConfiguration : BaseEntityConfiguration<Province, short>
{
    public override void Configure(EntityTypeBuilder<Province> builder) {
        base.Configure(builder);

        builder.ToTable("Provinces", "base");
        builder.Property(e => e.Title).HasMaxLength(1000);

        builder.Ignore(b => b.CreatedByDelegatedId);
        builder.Ignore(b => b.CreatedById);
        builder.Ignore(b => b.CreatedOn);
        builder.Ignore(b => b.Description);
        builder.Ignore(b => b.LastUpdatedByDelegatedId);
        builder.Ignore(b => b.LastUpdatedById);
        builder.Ignore(b => b.UpdatedOn);
    }
}

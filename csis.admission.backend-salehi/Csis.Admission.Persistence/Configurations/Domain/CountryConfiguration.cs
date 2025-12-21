using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class CountryConfiguration : BaseEntityConfiguration<Country,short>
{
    public override void Configure(EntityTypeBuilder<Country> builder) {
        base.Configure(builder);

        builder.ToTable("Countries", "base");
        builder.Property(e => e.Title).HasMaxLength(1000);
    }
}

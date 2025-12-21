using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class TownConfiguration : BaseEntityConfiguration<Town, short>
{
    public override void Configure(EntityTypeBuilder<Town> builder) {
        base.Configure(builder);

        builder.ToTable("Towns","base");
        builder.Property(e => e.Title).HasMaxLength(1000);
    }
}

using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class EliteTypeConfiguration : BaseEntityConfiguration<EliteType,short>
{
    public override void Configure(EntityTypeBuilder<EliteType> builder) {
        base.Configure(builder);

        builder.ToTable("EliteTypes","base");
    }
}

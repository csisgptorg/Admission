using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class ExcellentEducationLevelConfiguration : BaseEntityConfiguration<ExcellentEducationLevel,short>
{
    public override void Configure(EntityTypeBuilder<ExcellentEducationLevel> builder) {
        base.Configure(builder);

        builder.ToTable("ExcellentEducationLevels", "base");
        builder.Property(e => e.Title).HasMaxLength(1000);
    }
}

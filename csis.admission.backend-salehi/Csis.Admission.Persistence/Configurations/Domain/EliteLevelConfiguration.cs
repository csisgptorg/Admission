using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;
internal sealed class EliteLevelConfiguration : BaseEntityConfiguration<EliteLevel,short>
{
    public override void Configure(EntityTypeBuilder<EliteLevel> builder) {
        base.Configure(builder);

        builder.ToTable("EliteLevels", "base");

        builder.Ignore(b => b.CreatedByDelegatedId);
        builder.Ignore(b => b.CreatedById);
        builder.Ignore(b => b.CreatedOn);
        builder.Ignore(b => b.Description);
        builder.Ignore(b => b.LastUpdatedByDelegatedId);
        builder.Ignore(b => b.LastUpdatedById);
        builder.Ignore(b => b.UpdatedOn);

    }
}

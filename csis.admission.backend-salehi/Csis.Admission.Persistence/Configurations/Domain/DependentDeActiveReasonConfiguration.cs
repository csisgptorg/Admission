using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class DependentDeActiveReasonConfiguration : BaseEntityConfiguration<DependentDeActiveReason,short>
{
    public override void Configure(EntityTypeBuilder<DependentDeActiveReason> builder) {
        base.Configure(builder);

        builder.ToTable("DependentDeActiveReason","Base");

        builder.Ignore("CreatedById");
        builder.Ignore("CreatedOn");
        builder.Ignore("DeletedById");
        builder.Ignore("DeletedOn");
        builder.Ignore("Description");
        builder.Ignore("LastUpdatedById");
        builder.Ignore("UpdatedOn");
        builder.Ignore("CreatedByDelegatedId");
        builder.Ignore("LastUpdatedByDelegatedId");

        builder.Property(x => x.Title).HasMaxLength(100);
        builder.Property(x => x.Type).HasMaxLength(100);
    }
}

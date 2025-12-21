using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class TargetScoreConfiguration : BaseEntityConfiguration<TargetScore>
{
    public override void Configure(EntityTypeBuilder<TargetScore> builder) {

        base.Configure(builder);
        
        builder.ToTable("TargetScore", "stu");

        builder.Ignore("CreatedById");
        builder.Ignore("CreatedOn");
        builder.Ignore("DeletedById");
        builder.Ignore("DeletedOn");
        builder.Ignore("Description");
        builder.Ignore("LastUpdatedById");
        builder.Ignore("UpdatedOn");
        builder.Ignore("CreatedByDelegatedId");
        builder.Ignore("LastUpdatedByDelegatedId");
    }
}

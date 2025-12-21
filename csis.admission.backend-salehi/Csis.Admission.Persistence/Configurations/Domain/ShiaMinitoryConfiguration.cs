using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class ShiaMinitoryConfiguration : IEntityTypeConfiguration<ShiaMinitory>
{
    public void Configure(EntityTypeBuilder<ShiaMinitory> builder) {
        builder.ToTable("VShiaMinitory", "stu");
        builder.HasNoKey();

        builder.Ignore(builder => builder.Id);
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

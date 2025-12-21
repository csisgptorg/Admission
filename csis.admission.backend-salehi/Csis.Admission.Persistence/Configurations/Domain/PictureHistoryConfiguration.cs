using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class PictureHistoryConfiguration : SoftDeletedBaseEntityConfiguration<PictureHistory>
{
    public override void Configure(EntityTypeBuilder<PictureHistory> builder) {
        base.Configure(builder);
    }
}

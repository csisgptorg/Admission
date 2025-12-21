using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class BlockedStudentReasonConfiguration : SoftDeletedBaseEntityConfiguration<BlockedStudentReason>
{
    public override void Configure(EntityTypeBuilder<BlockedStudentReason> builder) {
        base.Configure(builder);
    }
}

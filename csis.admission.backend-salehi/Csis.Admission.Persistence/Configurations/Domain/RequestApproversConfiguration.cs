using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;
internal sealed class RequestApproverConfiguration : SoftDeletedBaseEntityConfiguration<RequestApprover,long>
{
    public override void Configure(EntityTypeBuilder<RequestApprover> builder) {
        base.Configure(builder);
    }
}

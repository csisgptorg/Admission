using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class RequestDocumentConfiguration : SoftDeletedBaseEntityConfiguration<RequestDocument, long>
{
    public override void Configure(EntityTypeBuilder<RequestDocument> builder) {
        base.Configure(builder);
        builder.HasOne(x => x.Request).WithMany(x => x.Documents).HasForeignKey(x => x.RequestId);
    }
}

using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class RequestConfiguration : SoftDeletedBaseEntityConfiguration<Request,long>
{
    public override void Configure(EntityTypeBuilder<Request> builder) {
        base.Configure(builder);

        builder.Property(x => x.Description).HasMaxLength(4000);
    }
}

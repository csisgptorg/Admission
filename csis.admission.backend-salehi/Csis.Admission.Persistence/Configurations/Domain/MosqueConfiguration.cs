using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class MosqueConfiguration : SoftDeletedBaseEntityConfiguration<Mosque>
{
    public override void Configure(EntityTypeBuilder<Mosque> builder) {
        base.Configure(builder);

        builder.Ignore(x=>x.MosqueActivityId);
        //builder.Ignore(x => x.MosqueAddressId);
    }
}


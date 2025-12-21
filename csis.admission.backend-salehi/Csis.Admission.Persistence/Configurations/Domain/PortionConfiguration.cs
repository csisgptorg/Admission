using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class PortionConfiguration : BaseEntityConfiguration<Portion,short> 
{
    public override void Configure(EntityTypeBuilder<Portion> builder) {
        base.Configure(builder);

        builder.ToTable("Portions","base");
    }
}

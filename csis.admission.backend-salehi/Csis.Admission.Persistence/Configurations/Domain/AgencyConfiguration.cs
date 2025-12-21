using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class AgencyConfiguration : BaseEntityConfiguration<Agency,short> 
{
    public override void Configure(EntityTypeBuilder<Agency> builder) {
        base.Configure(builder);
        builder.ToTable("Agencies", "Base");
    }
}

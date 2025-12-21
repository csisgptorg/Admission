using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;
internal sealed class CsisServiceConfiguration : BaseEntityConfiguration<CsisService>
{
    public override void Configure(EntityTypeBuilder<CsisService> builder) {
        base.Configure(builder);
        builder.ToTable("EmtiazOnvan");
        builder.Property(x => x.Title).HasMaxLength(50).HasColumnName("Onvan");
    }
}

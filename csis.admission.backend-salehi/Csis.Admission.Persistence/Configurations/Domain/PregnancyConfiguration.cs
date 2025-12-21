using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class PregnancyConfiguration : AuditableSoftDeletedEntityConfiguration<Pregnancy>
{
    public override void Configure(EntityTypeBuilder<Pregnancy> builder) {
        base.Configure(builder);
        builder.ToTable("Pregnancy");
    }
}

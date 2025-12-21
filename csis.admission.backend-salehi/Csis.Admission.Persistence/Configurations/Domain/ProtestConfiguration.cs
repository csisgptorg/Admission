using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;
internal sealed class ProtestConfiguration : AuditableSoftDeletedEntityConfiguration<Protest, long>
{
    public override void Configure(EntityTypeBuilder<Protest> builder) {
        base.Configure(builder);

        builder.ToTable("TbEteraz");
    }
}

using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;
internal sealed class TenantConfiguration : AuditableSoftDeletedEntityConfiguration<Tenant>
{
    public override void Configure(EntityTypeBuilder<Tenant> builder) {
        base.Configure(builder);

        builder.ToTable("tbTenant");
        builder.Property(x => x.Deleted).HasDefaultValue(false);
    }
}

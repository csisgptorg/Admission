using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class EducationConfiguration : AuditableEntityConfiguration<Education>
{
    public override void Configure(EntityTypeBuilder<Education> builder) {
        base.Configure(builder);
    }
}

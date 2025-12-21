using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class UniversityDegreeConfiguration : AuditableEntityConfiguration<UniversityDegree>
{
    public override void Configure(EntityTypeBuilder<UniversityDegree> builder) {
        base.Configure(builder);

        builder.ToTable("UniversityDegree", "base");
    }
}

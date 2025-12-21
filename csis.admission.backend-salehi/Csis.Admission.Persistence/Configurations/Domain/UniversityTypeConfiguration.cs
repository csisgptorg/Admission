using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class UniversityTypeConfiguration : BaseEntityConfiguration<UniversityType, short>
{
    public override void Configure(EntityTypeBuilder<UniversityType> builder) {
        base.Configure(builder);

        builder.ToTable("UniversityType", "base");
    }
}

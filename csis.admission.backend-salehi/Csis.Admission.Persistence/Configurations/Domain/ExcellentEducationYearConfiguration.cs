using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class ExcellentEducationYearConfiguration : BaseEntityConfiguration<ExcellentEducationYear,short>
{
    public override void Configure(EntityTypeBuilder<ExcellentEducationYear> builder) {
        base.Configure(builder);

        builder.ToTable("ExcellentEducationYears", "base");
    }
}

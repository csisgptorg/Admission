using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class StudentNameConfiguration : BaseEntityConfiguration<StudentName>
{
    public override void Configure(EntityTypeBuilder<StudentName> builder) {
        base.Configure(builder);
    }
}

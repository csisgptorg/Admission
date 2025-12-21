using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class SoldierStudentConfiguration : AuditableSoftDeletedEntityConfiguration<SoldierStudent>
{
    public override void Configure(EntityTypeBuilder<SoldierStudent> builder) {
        base.Configure(builder);
        builder.ToTable("SoldierStudent");
    }
}

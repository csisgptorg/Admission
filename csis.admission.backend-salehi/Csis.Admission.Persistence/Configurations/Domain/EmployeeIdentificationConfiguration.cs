using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

//EmployeeIdentification
internal sealed class EmployeeIdentificationConfiguration : SoftDeletedBaseEntityConfiguration<EmployeeIdentification>
{
    public override void Configure(EntityTypeBuilder<EmployeeIdentification> builder) {
        base.Configure(builder);

        builder.ToTable("TbEmployeeIdentification", "dbo");
        builder.Property(x => x.PersonnelId).HasColumnName("IdKarbar");
    }
}

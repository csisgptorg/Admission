using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class EmployeeViewStudentLogConfiguration : BaseEntityConfiguration<EmployeeViewStudentLog, long>
{
    public override void Configure(EntityTypeBuilder<EmployeeViewStudentLog> builder) {
        base.Configure(builder);
        builder.ToTable("TbViewLog");
        builder.Property(x => x.PersonnelId).HasColumnName("IDKarbar");
        builder.Property(x => x.Date).HasColumnName("ViewDate");
        builder.Property(x => x.Time).HasColumnName("ViewTime");
    }
}

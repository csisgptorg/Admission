using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class StudentReportConfiguration : BaseEntityConfiguration<CompleteStudentInfo>
{
    public override void Configure(EntityTypeBuilder<CompleteStudentInfo> builder) {
        base.Configure(builder);

        builder.ToTable("TotalStudentReport", "stu");

        builder.Ignore("CreatedById");
        builder.Ignore("CreatedOn");
        builder.Ignore("DeletedById");
        builder.Ignore("DeletedOn");
        builder.Ignore("Description");
        builder.Ignore("LastUpdatedById");
        builder.Ignore("UpdatedOn");
        builder.Ignore("CreatedByDelegatedId");
        builder.Ignore("LastUpdatedByDelegatedId");

        builder.Property(x => x.FirstName).HasColumnType("varchar(40)");
        builder.Property(x => x.LastName).HasColumnType("varchar(35)");
    }
}

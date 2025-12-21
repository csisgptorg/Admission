using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class DependentSummaryConfiguration : BaseEntityConfiguration<DependentSummary,long>
{
    public override void Configure(EntityTypeBuilder<DependentSummary> builder) {
        base.Configure(builder);

        builder.ToTable("DependentSummary","stu");

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
        builder.Property(x => x.FatherName).HasColumnType("varchar(30)");
        builder.Property(x => x.FatherName).HasColumnType("varchar(30)");
        builder.Property(x => x.CaseDescription).HasColumnType("varchar(300)");
    }
}

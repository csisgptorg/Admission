using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class ReportBuilderConfiguration : BaseEntityConfiguration<ReportBuilder,long>
{
    public override void Configure(EntityTypeBuilder<ReportBuilder> builder) {
        base.Configure(builder);
    }
}

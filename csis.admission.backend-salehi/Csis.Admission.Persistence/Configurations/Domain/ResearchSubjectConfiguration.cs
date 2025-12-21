using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class ResearchSubjectConfiguration : BaseEntityConfiguration<ResearchSubject,short>
{
    public override void Configure(EntityTypeBuilder<ResearchSubject> builder) {
        base.Configure(builder);

        builder.ToTable("ResearchSubjects", "base");
        builder.Property(e => e.Title).HasMaxLength(1000);
    }
}

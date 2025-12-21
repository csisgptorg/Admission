using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class ReligiousRoleQuestionConfiguration : SoftDeletedBaseEntityConfiguration<ReligiousRoleQuestion>
{
    public override void Configure(EntityTypeBuilder<ReligiousRoleQuestion> builder) {
        base.Configure(builder);

        builder.ToTable("ReligiousRoleQuestion");

        builder.Property(x => x.Codm).IsRequired();
        builder.Property(x => x.ReligiouslyDressedDescription).IsRequired().HasMaxLength(2000);
        builder.Property(x => x.HasRoleDescription).IsRequired().HasMaxLength(2000);
        builder.Property(x => x.HasRoleDescription).HasMaxLength(2000);
        builder.Property(x => x.Description).HasMaxLength(4000);
    }
}

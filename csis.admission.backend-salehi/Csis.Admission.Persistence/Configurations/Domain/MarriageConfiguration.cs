using Csis.Admission.Domain.Entities;
using Csis.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class MarriageConfiguration : SoftDeletedBaseEntityConfiguration<Marriage>
{
    public override void Configure(EntityTypeBuilder<Marriage> builder) {
        base.Configure(builder);

        builder.HasKey(e => e.Id).HasName("PK_Marriage");

        builder.Property(e => e.MarriageDate)
            .HasConversion(x => x.HasValue ? (int?) x.Value.ToPersianInteger() : null,
                x => x.HasValue ? x.Value.ToDateOnly() : null);

        builder.Property(e => e.DivorceDate)
            .HasConversion(x => x.HasValue ? (int?) x.Value.ToPersianInteger() : null,
                x => x.HasValue ? x.Value.ToDateOnly() : null);

        builder.Property(e => e.DeathDate)
            .HasConversion(x => x.HasValue ? (int?) x.Value.ToPersianInteger() : null,
                x => x.HasValue ? x.Value.ToDateOnly() : null);

        builder.HasOne(d => d.HusbandPerson).WithMany(p => p.MarriageHusbandPeople)
            .HasForeignKey(d => d.HusbandPersonId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Marriage_Person_husband");

        builder.HasOne(d => d.WifePerson).WithMany(p => p.MarriageWifePeople)
            .HasForeignKey(d => d.WifePersonId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Marriage_Person_wife");
    }
}

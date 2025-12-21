using Csis.Admission.Domain.Entities;
using Csis.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class NonStudentDependentConfiguration : SoftDeletedBaseEntityConfiguration<NonStudentDependant>
{
    public override void Configure(EntityTypeBuilder<NonStudentDependant> builder) {
        base.Configure(builder);

        builder.HasKey(e => e.Id).HasName("PK_NonStudentDependent");

        builder.Property(e => e.Description)
            .HasMaxLength(500);

        builder.Property(e => e.CaseCreateDate)
            .HasConversion(x => x.ToPersianInteger(),
                x => x.ToDateOnly());

        builder.Property(e => e.CaseDeactiveDate)
            .HasConversion(x => x.HasValue ? (int?) x.Value.ToPersianInteger() : null,
                x => x.HasValue ? x.Value.ToDateOnly() : null);

        builder.HasOne(d => d.Householder).WithMany(p => p.NonStudentDependents)
            .HasForeignKey(d => d.NonStudentCodm)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_NonStudentDependent_NonStudentCodm");
    }
}

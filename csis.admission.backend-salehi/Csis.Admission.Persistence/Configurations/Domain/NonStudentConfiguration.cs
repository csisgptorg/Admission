using Csis.Admission.Domain.Entities;
using Csis.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class NonStudentConfiguration : SoftDeletedBaseEntityConfiguration<NonStudent, long>
{
    public override void Configure(EntityTypeBuilder<NonStudent> builder) {
        base.Configure(builder);

        builder.Property(x => x.Id).HasColumnName("Codm");
        builder.HasKey(e => e.Id).HasName("PK_NonStudent");

        //builder.Property(e => e.Codm).ValueGeneratedNever();
        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.Description).HasMaxLength(500);

        builder.Property(e => e.CaseCreateDate)
            .HasConversion(x => x.ToPersianInteger(),
                x => x.ToDateOnly());

        builder.Property(e => e.CaseExpireDate)
            .HasConversion(x => x.HasValue ? (int?) x.Value.ToPersianInteger() : null,
                x => x.HasValue ? x.Value.ToDateOnly() : null);

        builder.Property(e => e.CaseBlockDate)
            .HasConversion(x => x.HasValue ? (int?) x.Value.ToPersianInteger() : null,
                x => x.HasValue ? x.Value.ToDateOnly() : null);

        //builder.HasOne(d => d.Person).WithMany(p => p.NonStudents)
        //    .HasForeignKey(d => d.PersonId)
        //    .OnDelete(DeleteBehavior.Restrict)
        //    .HasConstraintName("FK_NonStudent_PersonId");
    }
}

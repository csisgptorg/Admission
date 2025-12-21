using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class StudentConfiguration : SoftDeletedBaseEntityConfiguration<Student>
{
    public override void Configure(EntityTypeBuilder<Student> builder) {
        base.Configure(builder);
    }
}

//internal sealed class StudentConfiguration : AuditableSoftDeletedEntityConfiguration<Student>
//{
//    public override void Configure(EntityTypeBuilder<Student> builder) {
//        base.Configure(builder);

//        builder.HasKey(e => e.Id).HasName("PK_Student");

//        builder.Property(e => e.Id).HasColumnName("Codm");
//        builder.Property(e => e.Description).HasMaxLength(500);

//        builder.Property(x => x.CaseBlockReasons)
//        .HasConversion(
//            x => x == null ? null : string.Join(',', x.Select(e => (int) e)),
//            x => string.IsNullOrWhiteSpace(x) ? new() :
//                x.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Select(x => (CaseBlockReason) x).ToList())
//        .Metadata
//        .SetValueComparer(new ValueComparer<List<CaseBlockReason>>(
//            (c1, c2) => c1.SequenceEqual(c2),
//            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
//            c => c));

//        builder.Property(x => x.CaseExtensionReasons)
//        .HasConversion(
//            x => x == null ? null : string.Join(',', x.Select(e => (int) e)),
//            x => string.IsNullOrWhiteSpace(x) ? new() :
//                x.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Select(x => (CaseExtensionReason) x).ToList())
//        .Metadata
//        .SetValueComparer(new ValueComparer<List<CaseExtensionReason>>(
//            (c1, c2) => c1.SequenceEqual(c2),
//            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
//            c => c));

//        builder.HasOne(d => d.Person).WithMany(p => p.Students)
//            .HasForeignKey(d => d.PersonId)
//            .HasConstraintName("FK_Student_PersonID");
//    }
//}

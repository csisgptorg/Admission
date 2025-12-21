using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class MemorizerConfiguration : AuditableSoftDeletedEntityConfiguration<Memorizer>
{
    public override void Configure(EntityTypeBuilder<Memorizer> builder) {
        base.Configure(builder);
        builder.ToTable("TbHafezin");
        builder.Property(x => x.Deleted).HasDefaultValue(false);
        builder.Property(x => x.DependentId).HasColumnName("IDTakaffol");
        builder.Property(x => x.ApprovalCenter).HasColumnName("markazeHouzavi");

        builder.HasOne(x => x.Dependent).WithMany().HasForeignKey(x => x.DependentId);
    }
}

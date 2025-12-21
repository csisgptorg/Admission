using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;
internal sealed class ResearchConfiguration : AuditableSoftDeletedEntityConfiguration<Research>
{
    public override void Configure(EntityTypeBuilder<Research> builder) {
        base.Configure(builder);

        builder.ToTable("TbResearch");
        builder.Property(x =>x.Deleted).HasDefaultValue(false);

        builder.Property(x=>x.Title).HasMaxLength(200);
        builder.Property(x=>x.Type).HasColumnName("researchType");
        builder.Property(x=>x.BookShabak).HasMaxLength(100);
        builder.Property(x=>x.SubjectId).HasColumnName("Subject");
        builder.Property(x=>x.BookPublisher).HasColumnName("TitlePublisher").HasMaxLength(100);
        builder.Property(x=>x.ArticlePublication).HasColumnName("TitleMag").HasMaxLength(100);
        builder.Property(x=>x.ProjectEmployer).HasColumnName("karfarma").HasMaxLength(300);
    }
}

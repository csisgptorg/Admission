/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Domain.Common;
using Humanizer;

namespace Csis.Admission.Persistence.Configurations;

internal class BaseEntityConfiguration<T, TKey> : IEntityTypeConfiguration<T> where T : class, IEntity<TKey>
{
    public virtual void Configure(EntityTypeBuilder<T> builder) {
        builder.HasKey(e => e.Id);

        builder.ToTable(builder.Metadata.GetTableName().Pluralize());

        builder.Property(e => e.Description).IsRequired(false).HasMaxLength(2000);
        builder.Property(e => e.CreatedOn).IsRequired();
        builder.Property(e => e.UpdatedOn).IsRequired(false);
        builder.Property(e => e.CreatedById).IsRequired(false);
        builder.Property(e => e.LastUpdatedById).IsRequired(false);
    }
}

internal class BaseEntityConfiguration<T> : BaseEntityConfiguration<T, int>, IEntityTypeConfiguration<T>
    where T : class, IEntity
{ }

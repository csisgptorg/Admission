/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Domain.Common;

namespace Csis.Admission.Persistence.Configurations;

internal class SoftDeletedBaseEntityConfiguration<T, TKey> : BaseEntityConfiguration<T, TKey>, IEntityTypeConfiguration<T>
    where T : class, ISoftDeletedEntity<TKey>
{
    public new virtual void Configure(EntityTypeBuilder<T> builder) {
        base.Configure(builder);

        builder.Property(e => e.Deleted).HasDefaultValue(false);
        builder.Property(e => e.DeletedOn).IsRequired(false);

        builder.HasQueryFilter(x => !x.Deleted);
    }
}

internal class SoftDeletedBaseEntityConfiguration<T> : SoftDeletedBaseEntityConfiguration<T, int>, IEntityTypeConfiguration<T>
    where T : class, ISoftDeletedEntity
{ }

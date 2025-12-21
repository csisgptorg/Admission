using Csis.Admission.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations;

internal class AuditableSoftDeletedEntityConfiguration<T, TKey> : SoftDeletedBaseEntityConfiguration<T, TKey>, IEntityTypeConfiguration<T>
    where T : class, ISoftDeletedEntity<TKey>, IAuditable
{
    public new virtual void Configure(EntityTypeBuilder<T> builder) {
        base.Configure(builder);

        builder.Ignore(e => e.AuditDataSource);
        builder.Ignore(e => e.AuditRequestId);
        builder.Ignore(e => e.AuditPersonId);
        builder.Ignore(e => e.TempId);
    }
}

internal class AuditableSoftDeletedEntityConfiguration<T> : AuditableSoftDeletedEntityConfiguration<T, int>, IEntityTypeConfiguration<T>
    where T : class, ISoftDeletedEntity, IAuditable
{ }

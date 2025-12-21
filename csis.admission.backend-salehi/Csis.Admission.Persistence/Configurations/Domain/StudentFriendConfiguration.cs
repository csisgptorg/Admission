using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class StudentFriendConfiguration : AuditableSoftDeletedEntityConfiguration<StudentFriend>
{
    public override void Configure(EntityTypeBuilder<StudentFriend> builder) {
        base.Configure(builder);
        builder.ToTable("TbFriend");
        builder.Property(e => e.FriendCodm).HasColumnName("FCodm");
        builder.Property(e => e.Mobile).HasColumnName("Mobile");
        builder.Property(e => e.FirstName).HasColumnName("Name");
        builder.Property(e => e.LastName).HasColumnName("Family");
    }
}

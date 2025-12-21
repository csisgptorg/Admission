using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class CsisWSMLogConfiguration : SoftDeletedBaseEntityConfiguration<CsisWSMLog,long>
{
    public override void Configure(EntityTypeBuilder<CsisWSMLog> builder) {
        base.Configure(builder);

        builder.Property(e => e.NationalCode).HasMaxLength(15);
        builder.Property(e => e.YektaCode).HasMaxLength(20);
        builder.Property(x => x.ApprovalCenter).HasMaxLength(200);
        builder.Property(x => x.CaseNumberInApprovalCenter).HasMaxLength(20);
        builder.Property(x => x.DataGroup).HasMaxLength(5);
        builder.Property(x => x.PhoneNumber).HasMaxLength(20);
    }
}

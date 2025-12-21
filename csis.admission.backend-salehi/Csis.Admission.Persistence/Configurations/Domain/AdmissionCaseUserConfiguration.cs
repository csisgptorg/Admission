using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class AdmissionCaseUserConfiguration : SoftDeletedBaseEntityConfiguration<AdmissionCaseUser, Guid>
{
    public override void Configure(EntityTypeBuilder<AdmissionCaseUser> builder) {
        base.Configure(builder);
        builder.ToTable("StudentPreCaseFilings");
        builder.Property(x => x.BirthDate).IsRequired();
        builder.Property(x => x.Mobile).IsRequired();
        builder.Property(x => x.Codm)
            .HasColumnName("TempCodm")
            .ValueGeneratedOnAdd()
            .HasColumnType("int");
    }
}

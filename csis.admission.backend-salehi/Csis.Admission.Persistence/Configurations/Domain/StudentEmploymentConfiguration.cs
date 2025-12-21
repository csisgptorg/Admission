using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class StudentEmploymentConfiguration : AuditableSoftDeletedEntityConfiguration<StudentEmployment>
{
    public override void Configure(EntityTypeBuilder<StudentEmployment> builder) {
        base.Configure(builder);

        builder.ToTable("tbEmployee");

        builder.Property(x => x.HasIncome).HasColumnName("Question1");
        builder.Property(x => x.IsEmployee).HasColumnName("Question2");
        builder.Property(x => x.EmployeeName).HasColumnName("NameEmployee").HasMaxLength(200);
        builder.Property(x => x.EmployeeAddress).HasColumnName("AddressEmployee").HasMaxLength(300);
        builder.Property(x => x.HasSufficientIncome).HasColumnName("Question3");
        builder.Property(x => x.HasAnotherBaseInsurance).HasColumnName("Question4");
        builder.Property(x => x.InsurancePlaceName).HasColumnName("NameInsurancePlace").HasMaxLength(200);
        builder.Property(x => x.InsurancePlaceAddress).HasColumnName("AddressInsurancePlace").HasMaxLength(300);
        builder.Property(x => x.HasAnotherSupInsurance).HasColumnName("Question5");
        builder.Property(x => x.IsEmployeeInHowze).HasColumnName("Kadr");
        builder.Property(x => x.HowzeTypeId).HasColumnName("KadrType");
        builder.Property(x => x.IsRetried).HasColumnName("Bazneshaste");
        builder.Property(x => x.InsuranceTypeId).HasColumnName("KindBimeh");
        builder.Property(x => x.Reference).HasColumnName("Refrence");
        builder.Property(x => x.Decile).HasColumnName("Dahak");
    }
}

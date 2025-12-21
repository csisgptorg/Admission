using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;
internal sealed class DependentEmploymentConfiguration : AuditableSoftDeletedEntityConfiguration<DependentEmployment>
{
    public override void Configure(EntityTypeBuilder<DependentEmployment> builder) {
        base.Configure(builder);

        builder.ToTable("TbEmployeeTakaffol");
        
        builder.Property(x => x.DependentId).HasColumnName("IDTakaffol");
        builder.Property(x => x.IsEmployee).HasColumnName("question2");
        builder.Property(x => x.EmployeeName).HasColumnName("NameEmployee").HasMaxLength(200);
        builder.Property(x => x.EmployeeAddress).HasColumnName("AddressEmployee").HasMaxLength(300);
        builder.Property(x => x.HasAnotherBaseInsurance).HasColumnName("question4");
        builder.Property(x => x.InsurancePlaceName).HasColumnName("NameInsurancePlace").HasMaxLength(200);
        builder.Property(x => x.InsurancePlaceAddress).HasColumnName("AddressInsurancePlace").HasMaxLength(300);
        builder.Property(x => x.HasAnotherSupInsurance).HasColumnName("question5");
        builder.Property(x => x.InsuranceType).HasColumnName("KindBimeh");
        builder.Property(x => x.Decile).HasColumnName("Dahak");

        builder.HasOne(x=>x.Dependent).WithMany().HasForeignKey(x => x.DependentId);
    }
}

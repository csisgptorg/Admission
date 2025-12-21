using Csis.Admission.Domain.Common;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csis.Admission.Persistence.Configurations.Domain;
//internal sealed class EmploymentConfiguration : SoftDeletedBaseEntityConfiguration<Employment>
//{    
//    public override void Configure(EntityTypeBuilder<Employment> builder) {
//        base.Configure(builder);

//        builder.HasKey(e => e.StudentCodm).HasName("PK_Employment");

//        builder.Property(e => e.StudentCodm).ValueGeneratedNever();
//        builder.Property(e => e.EmploymentAddress)
//            .HasMaxLength(300)
//            .IsUnicode(false);
//        builder.Property(e => e.EmploymentName)
//            .HasMaxLength(200)
//            .IsUnicode(false);
//        builder.Property(e => e.InsurancePlaceAddress)
//            .HasMaxLength(300)
//            .IsUnicode(false);
//        builder.Property(e => e.InsurancePlaceName)
//            .HasMaxLength(200)
//            .IsUnicode(false);
//    }
//}

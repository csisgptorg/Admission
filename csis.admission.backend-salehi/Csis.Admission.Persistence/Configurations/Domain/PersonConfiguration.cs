using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csis.Admission.Persistence.Configurations.Domain;

internal sealed class PersonConfiguration : SoftDeletedBaseEntityConfiguration<Person>
{
    public override void Configure(EntityTypeBuilder<Person> builder) {
        base.Configure(builder);

        builder.HasKey(e => e.Id).HasName("PK_Person");

        builder.Property(e => e.Id).HasComment("شناسه فرد");
        builder.Property(e => e.BankAccountNumber)
            .HasMaxLength(13)
            .IsUnicode(false)
            .HasComment("شماره حساب");
        builder.Property(e => e.ShebaNumber)
            .HasMaxLength(26)
            .IsUnicode(false)
            .HasComment("شماره شبا");
        builder.Property(e => e.BirthCertDescription)
            .HasMaxLength(100)
            .HasComment("توضیحات شناسنامه");
        builder.Property(e => e.BirthCertIssuePlace)
            .HasMaxLength(50)
            .HasComment("محل صدور شناسنامه");
        builder.Property(e => e.BirthCertIssueProvince)
            .HasMaxLength(50)
            .HasComment("استان محل صدور شناسنامه");
        builder.Property(e => e.BirthCertNumber)
            .HasMaxLength(10)
            .IsUnicode(false)
            .HasComment("شماره شناسنامه");
        builder.Property(e => e.BirthCertSeri)
            .HasMaxLength(6)
            .HasComment("سری شناسنامه");
        builder.Property(e => e.BirthCertSerial).HasComment("سریال شناسنامه");

        builder.Property(e => e.BirthDate).HasComment("تاریخ تولد");

        builder.Property(e => e.Citizenship).HasComment("تابعیت");
        builder.Property(e => e.DeathCause).HasComment("علت فوت");

        builder.Property(e => e.DeathDate).HasComment("تاریخ فوت");

        builder.Property(e => e.FatherName)
            .HasMaxLength(30)
            .HasComment("نام پدر");
        builder.Property(e => e.FidaCode)
            .HasMaxLength(12)
            .IsUnicode(false)
            .HasComment("شناسه فیدا");
        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(40)
            .HasComment("نام");
        builder.Property(e => e.Gender).HasComment("جنسیت");
        builder.Property(e => e.IsDead).HasComment("مرحوم است");
        builder.Property(e => e.IsSadat).HasComment("سیادت");
        builder.Property(e => e.LastName)
            .HasMaxLength(35)
            .HasComment("نام خانوادگی");

        builder.Property(e => e.Mobile)
            .HasMaxLength(11)
            .IsUnicode(false)
            .HasComment("تلفن همراه");
        builder.Property(e => e.NationalCode)
            .HasMaxLength(10)
            .IsUnicode(false)
            .IsFixedLength()
            .HasComment("کد ملی");
        builder.Property(e => e.Nationality).HasComment("ملیت");

        builder.Property(e => e.PassportNumber)
            .HasMaxLength(20)
            .IsUnicode(false)
            .HasComment("شماره پاسپورت");
        builder.Property(e => e.Religion).HasComment("مذهب");

        builder.Property(e => e.ResidenceExpireDate).HasComment("تاریخ اعتبار اقامت");

        builder.Property(e => e.SabteAhvalConfirm).HasComment("مورد تایید ثبت احوال می باشد");
        builder.Property(e => e.YektaCode)
            .HasMaxLength(10)
            .IsUnicode(false)
            .HasComment("شناسه یکتا");

        builder.Property(e => e.UniqueCode).
            HasMaxLength(8)
            .IsUnicode(false)
            .HasComment("کد یکتا");

        builder.HasOne(x => x.FatherPerson)
            .WithMany()
            .HasForeignKey(x => x.FatherPersonId)
            .IsRequired(false);

        builder.HasOne(x => x.MotherPerson)
            .WithMany()
            .HasForeignKey(x => x.MotherPersonId)
            .IsRequired(false);
    }
}

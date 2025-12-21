using Csis.Admission.Application.Common;
using Csis.Admission.Application.Features.Addresses.Commands;
using Csis.Authorization.Models;
using FluentValidation;

namespace Csis.Admission.Application.Features.Addresses.Validators;

public sealed class CreateRequestStudentAddressCommandValidator : BaseValidator<CreateOrUpdateStudentAddressCommand>
{
    public CreateRequestStudentAddressCommandValidator() {
        RuleFor(x => x.Codm).Must(x => x > 0)
                .WithMessage("کد مرکز خدمات ")
                .WithName("کد مرکز خدمات");
        RuleFor(x => x.ProjectCode).Must(x => x > 0)
                .WithMessage("کد مرکز خدمات ")
                .WithName("کد مرکز خدمات");
        RuleFor(x => x.ProvinceId).Must(x => x > 0)
                .WithMessage("کد مرکز خدمات ")
                .WithName("کد مرکز خدمات");

        RuleFor(x => x.CityId).Must(x => x > 0)
                .WithMessage("کد مرکز خدمات ")
                .WithName("کد مرکز خدمات");

        RuleFor(x => x.PortionId).Must(x => x > 0)
                .WithMessage("کد مرکز خدمات ")
                .WithName("کد مرکز خدمات");

        RuleFor(x => new { x.TownId, x.RuralId }).Must(x => x.TownId > 0 ^ x.RuralId > 0)
                .WithMessage("ققط یک یکی از موارد شهر یا دهستان باید تکمیل گردد ")
                .WithName("شهر-دهستان");

        /*

    /// <inheritdoc/>
    public short? ProvinceId { get; set; }

    /// <summary>شهرستان </summary>
    public short? CityId { get; set; }

    /// <summary>بخش</summary>
    public short? PortionId { get; set; }

    /// <summary>شهر</summary>
    public short? TownId { get; set; }

    /// <summary>دهستان</summary>
    public short? RuralId { get; set; }

    /// <summary>شهرک</summary>
    public string Township { get; set; }

    /// <inheritdoc/>
    public string Village { get; set; }

    /// <summary>محله</summary>
    public string District { get; set; }

    /// <summary>خیابان اصلی</summary>
    public string Avenue { get; set; }

    /// <summary>خیابان فرعی</summary>
    public string Street { get; set; }

    /// <summary>کوچه اصلی</summary>
    public string Alley { get; set; }

    /// <summary>کوچه فرعی</summary>
    public string Lane { get; set; }

    /// <summary>پلاک</summary>
    public string Number { get; set; }

    /// <summary>مجتمع</summary>
    public string Complex { get; set; }

    /// <summary>بلوک</summary>
    public string Block { get; set; }

    /// <summary>واحد</summary>
    public string Unit { get; set; }

    /// <inheritdoc/>
    public short? Floor { get; set; }

    /// <inheritdoc/>
    public long? ZipCode { get; set; }

    /// <inheritdoc/>
    public string ConfirmDate { get; set; }

    /// <summary>همیشه یک</summary>
    public short ProjectCode { get; set; }

    /// <summary>همیشه یک</summary>
    public bool? Flag { get; set; }

         */
    }
}

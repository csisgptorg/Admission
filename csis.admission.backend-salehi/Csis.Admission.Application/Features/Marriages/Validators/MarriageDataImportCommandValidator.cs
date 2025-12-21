using Csis.Admission.Application.Features.Marriages.Commands;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Csis.Admission.Application.Features.Marriages.Validators;

public sealed class MarriageDataImportCommandValidator : BaseValidator<MarriageDataImportCommand>
{
    public MarriageDataImportCommandValidator() {
        RuleFor(x => x.Codm)
           .NotEmpty()
           .WithName("کد مرکز خدمات");

        RuleFor(x => x.MarriageDate)
            .NotEmpty()
            .Must(date => {
                var dateStr = date.ToString();
                return Regex.IsMatch(dateStr, @"^(13|14)\d{2}(0[1-9]|1[0-2])(0[1-9]|[12][0-9]|3[01])$");
            })
            .WithMessage("تاریخ ازدواج معتبر نیست.");
        
        RuleFor(x => x.SpouseNationalCode)
            .NotEmpty()
            .WithName("شماره ملی همسر");

        RuleFor(x => x.SpouseBirthDate)
            .NotEmpty()
            .Must(date => {
                var dateStr = date.ToString();
                return Regex.IsMatch(dateStr, @"^(13|14)\d{2}(0[1-9]|1[0-2])(0[1-9]|[12][0-9]|3[01])$");
            })
            .WithMessage("تاریخ تولد همسر معتبر نیست.");
    }
}

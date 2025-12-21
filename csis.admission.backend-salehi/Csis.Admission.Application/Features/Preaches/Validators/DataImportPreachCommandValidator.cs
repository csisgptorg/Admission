using FluentValidation;
using System.Text.RegularExpressions;
using Csis.Admission.Application.Features.Preaches.Commands;

namespace Csis.Admission.Application.Features.Preaches.Validators;

public sealed class DataImportPreachCommandValidator : BaseValidator<DataImportPreachCommand>
{
    public DataImportPreachCommandValidator() {

        RuleFor(x => x.StartDate)
            .Must(StartDate => Regex.IsMatch(StartDate, @"^(13[0-9]{2}|14[0-9]{2})[-\/](0[1-9]|1[0-2])[-\/](0[1-9]|[12][0-9]|3[01])$"))
            .WithMessage("فرمت تاریخ باید به صورت yyyy/MM/dd باشد.");

        RuleFor(x => x.EndDate)
             .Must(EndDate => string.IsNullOrWhiteSpace(EndDate) || Regex.IsMatch(EndDate, @"^(13[0-9]{2}|14[0-9]{2})[-\/](0[1-9]|1[0-2])[-\/](0[1-9]|[12][0-9]|3[01])$"))
             .WithMessage("فرمت تاریخ باید به صورت yyyy/MM/dd باشد.");


        RuleFor(x => x.RecordIdInApprovalCenter)
            .NotNull().WithMessage("این فیلد نباید خالی باشد.")
            .NotEmpty().WithMessage("این فیلد نباید خالی باشد.")
            .Must(val => val?.Trim() != "0").WithMessage("مقدار نباید صفر باشد.")
            .Must(val => !string.IsNullOrWhiteSpace(val)).WithMessage("این فیلد نباید فقط شامل فاصله باشد.");

    }
}

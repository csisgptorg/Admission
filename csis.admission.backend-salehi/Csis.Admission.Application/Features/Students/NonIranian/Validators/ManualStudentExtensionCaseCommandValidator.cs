using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.Students.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.Students.NonIranian.Validators;

// validation
internal sealed class ManualStudentExtensionCaseCommandValidator : AbstractValidator<ManualStudentExtensionCaseCommand>
{
    public ManualStudentExtensionCaseCommandValidator() {
        RuleFor(x => x.Codm)
            .GreaterThan(0).WithMessage("کد مرکز طلبه باید بزرگتر از صفر باشد.");

        RuleFor(x => x.CaseValidityReasonId)
            .NotEmpty().WithMessage("دلایل اعتبار پرونده نمی تواند خالی باشد.");

        RuleFor(x => x.CaseValidityDate.StringDateToInt())
            .GreaterThan(0).WithMessage("تاریخ اعتبار پرونده باید بزرگتر از صفر باشد.");
    }
}

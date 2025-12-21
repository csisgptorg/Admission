using Csis.Admission.Application.Features.BlockedServices.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.CaseBlock.Validators;

// validation
internal sealed class CreateStudentCaseUnblockRequestCommandValidator : AbstractValidator<CreateStudentCaseUnblockRequestCommand>
{
    public CreateStudentCaseUnblockRequestCommandValidator()
    {
        RuleFor(x => x.Codm)
            .GreaterThan(0).WithMessage("کد مرکز باید بزرگتر از صفر باشد.");
    }
}

using FluentValidation;
using Csis.Admission.Application.Features.BlockServices.Commands;

namespace Csis.Admission.Application.Features.CaseFilings.Validator;

internal sealed class CreateStudentBlockServiceCommandValidator : AbstractValidator<CreateStudentBlockServiceCommand>
{
    public CreateStudentBlockServiceCommandValidator() {
        RuleFor(x => x.Codm).NotEmpty().WithName("کد مرکز خدمات");
        RuleFor(x => x.BlockDate).NotEmpty().WithName("تاریخ انسداد");
        RuleFor(x => x.ServiceId).NotEmpty().WithName("خدمت");
        RuleFor(x => x.Reason).NotEmpty().WithName("علت");
    }
}

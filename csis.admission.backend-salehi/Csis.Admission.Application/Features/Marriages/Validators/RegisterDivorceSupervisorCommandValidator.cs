using Csis.Admission.Application.Features.Divorce.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.Marriages.Validators;

public sealed class RegisterDivorceSupervisorCommandValidator : BaseValidator<UpdateDependentDivorceCommand>
{
    public RegisterDivorceSupervisorCommandValidator() {
        RuleFor(x => x.DivorceDate).NotEmpty().WithName("تاریخ طلاق");
        RuleFor(x => x.DependentBirthDate).NotEmpty().WithName("تاریخ تولد همسر");
        RuleFor(x => x.DependentNationalCode).NotEmpty().WithName("کد ملی همسر");
    }
}

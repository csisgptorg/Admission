using Csis.Admission.Application.Features.Employments.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.Employments.Validators;

public sealed class EmployeeDataImportCommandValidator : BaseValidator<EmployeeDataImportCommand>
{
    public EmployeeDataImportCommandValidator() {
        RuleFor(x => x.Codm).NotEmpty().WithName("کد مرکز");
        RuleFor(x => x.IsEmployee).NotEmpty().Must(x=> x == true).WithName("شاغل");
        RuleFor(x => x.EmployeeName).NotEmpty().WithName("نام محل اشتغال");
        //RuleFor(x => x.HasAnotherBaseInsurance).NotEmpty().WithName("وضعیت بیمه");
        //RuleFor(x => x.InsurancePlaceName).NotEmpty().WithName("نام محل بیمه");
        //RuleFor(x => x.Reference).IsInEnum().WithName("روش شناسایی اشتغال");
    }
}

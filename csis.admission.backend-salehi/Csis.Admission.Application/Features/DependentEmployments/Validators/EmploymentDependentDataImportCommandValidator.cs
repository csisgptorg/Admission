using Csis.Admission.Application.Features.DependentEmployments.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.DependentEmployments.Validators;

public sealed class EmploymentDependentDataImportCommandValidator : BaseValidator<EmploymentDependentDataImportCommand>
{
    public EmploymentDependentDataImportCommandValidator() {
        RuleFor(x => x.Codm)
            .NotEmpty()
            .WithName("کد مرکز خدمات");

        RuleFor(x => x.DependentId)
            .NotEmpty()
            .WithName("شناسه تکفل");
        
        // ارتباط داده ای فقط می تواند ثبت اشتغال کند
        RuleFor(x => x.IsEmployee)
            .Must(x => x == true)
            .WithName("اشتغال");

        // ارتباط داده ای فقط می تواند ثبت اشتغال کند
        RuleFor(x => x.EmployeeName)
            .NotEmpty()
            .MaximumLength(200)
            .WithName("نام محل کار");


  

    }
}

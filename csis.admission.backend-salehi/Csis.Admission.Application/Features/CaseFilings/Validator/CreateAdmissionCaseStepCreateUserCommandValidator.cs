using Csis.Admission.Application.Features.CaseFilings.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.CaseFilings.Validator;

/// <summary>
/// اعتبارسنجی ساخت کاربر
/// </summary>
public sealed class CreateAdmissionCaseStepCreateUserCommandValidator : AbstractValidator<CreateAdmissionCaseStepCreateUserCommand>
{
    public CreateAdmissionCaseStepCreateUserCommandValidator() {
            
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("رمز عبور نمی‌تواند خالی باشد")
            .MinimumLength(8).WithMessage("رمز عبور باید حداقل ۸ کاراکتر باشد")
            .Matches("[A-Za-z]").WithMessage("رمز عبور باید حداقل یک حرف بزرگ یا کوچک داشته باشد")
            .Matches("[0-9]").WithMessage("رمز عبور باید حداقل یک عدد داشته باشد")
            .Matches("[^a-zA-Z0-9]").WithMessage("رمز عبور باید حداقل یک کاراکتر خاص داشته باشد");
    }
}

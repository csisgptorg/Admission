using Csis.Admission.Application.Features.Auth.Commands;
using Csis.Authorization.Models;
using FluentValidation;

namespace Csis.Admission.Application.Features.Auth.Validators;

/// <summary>
/// اعتبارسنجی کامند ورود به سامانه
/// </summary>
public sealed class LoginCommandValidator : BaseValidator<LoginCommand>
{
    /// <summary>
    /// 
    /// </summary>
    public LoginCommandValidator() {
        RuleFor(x => x.UserType).IsInEnum().WithName("نوع کاربر");

        When(x => x.UserType == UserType.Employee, () => {
            RuleFor(x => x.Username).NotEmpty().MaximumLength(64).WithName("نام کاربری");
            RuleFor(x => x.Password).NotEmpty().MaximumLength(32).WithName("کلمه عبور");
        });

        When(x => x.UserType == UserType.Student, () => {
            RuleFor(x => x.ExternalToken).NotEmpty().MaximumLength(128).WithName("توکن");
        });
    }
}

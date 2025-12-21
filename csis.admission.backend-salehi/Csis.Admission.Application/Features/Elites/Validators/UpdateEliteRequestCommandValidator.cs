using FluentValidation;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.Elites.Commands;

namespace Csis.Admission.Application.Features.Elites.Validators;

/// <summary>
/// اعتبارسنجی بروزرسانی نخبگان (درخواست)
/// </summary>
public sealed class UpdateEliteRequestCommandValidator : AbstractValidator<UpdateEliteRequestCommand>
{
    public UpdateEliteRequestCommandValidator() {

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("شناسه نخبه باید بزرگتر از صفر باشد.");
        RuleFor(x => x.Codm)
            .GreaterThan(0).WithMessage("کد مرکز خدمات باید بزرگتر از صفر باشد.");
        RuleFor(x => x.EliteTypeId)
            .NotNull().WithMessage("نوع نخبگی نمی‌تواند خالی باشد.");

        RuleFor(x => x.EliteLevelId)
            .NotNull().WithMessage("سطح نخبگی نمی‌تواند خالی باشد.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("تاریخ شروع نمی‌تواند خالی باشد.");

        RuleFor(x => x.EndDate.StringDateToInt())
            .NotNull().WithMessage("تاریخ پایان الزامی است.")
            .GreaterThan(x => x.StartDate.StringDateToInt()).WithMessage("تاریخ پایان باید بزرگتر از تاریخ شروع باشد.");

        RuleFor(x => x.ApprovalCenterTitle)
            .NotEmpty().WithMessage("مرجع تایید نمی‌تواند خالی باشد.")
            .MaximumLength(200).WithMessage("مرجع تایید نباید بیشتر از 200 کاراکتر باشد.");
    }

}

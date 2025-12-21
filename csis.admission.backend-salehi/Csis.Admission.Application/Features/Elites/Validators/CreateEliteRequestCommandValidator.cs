using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.Elites.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.Elites.Validators;

// validaton
internal sealed class CreateEliteRequestCommandValidator : AbstractValidator<CreateEliteRequestCommand>
{
    public CreateEliteRequestCommandValidator() {
        RuleFor(x => x.Codm)
            .GreaterThan(0).WithMessage("کد مرکز خدمات باید بزرگتر از صفر باشد.");

        RuleFor(x => x.EliteTypeId)
            .NotNull().WithMessage("نوع نخبگی الزامی است.");

        RuleFor(x => x.EliteLevelId)
            .NotNull().WithMessage("سطح نخبگی الزامی است.");

        RuleFor(x => x.StartDate)
            .NotNull().WithMessage("تاریخ شروع الزامی است.");

        RuleFor(x => x.EndDate.StringDateToInt())
            .NotNull().WithMessage("تاریخ پایان الزامی است.")
            .GreaterThan(x => x.StartDate.StringDateToInt()).WithMessage("تاریخ پایان باید بزرگتر از تاریخ شروع باشد.");

        RuleFor(x => x.ApprovalCenterTitle)
            .NotEmpty().WithMessage("مرجع تایید الزامی است.")
            .MaximumLength(200).WithMessage("مرجع تایید نباید بیشتر از 200 کاراکتر باشد.");
    }
}

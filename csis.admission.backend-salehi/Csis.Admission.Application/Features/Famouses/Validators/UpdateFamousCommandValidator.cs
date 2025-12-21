using Csis.Admission.Application.Features.Famouses.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.Famouses.Validators;

public sealed class UpdateFamousCommandValidator : BaseValidator<UpdateFamousRequestCommand>
{
    public UpdateFamousCommandValidator() {
        RuleFor(x => x.Type).NotEmpty().WithName("نوع");
        RuleFor(x => x.Area).NotEmpty().WithName("محدوده اشتهار");
        When(x => x.Type is not TypeEnum.ReligiousAuthorities and not TypeEnum.SpecialClergy, () => {
            RuleFor(x => x.Role).Null().WithMessage("فیلد عنوان فقط برای نوع مراجع و روحانیون ویژه مجاز است.");
        });
    }
}

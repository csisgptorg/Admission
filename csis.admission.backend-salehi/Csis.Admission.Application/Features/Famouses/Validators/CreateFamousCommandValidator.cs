using Csis.Admission.Application.Features.Famouses.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.Famouses.Validators;

/// <summary>
/// ایجاد ولیدیتور مشهور
/// </summary>
public sealed class CreateFamousCommandValidator : BaseValidator<CreateFamousRequestCommand>
{
    public CreateFamousCommandValidator() {
        RuleFor(x => x.Type).NotEmpty().WithName("نوع");
        RuleFor(x => x.Area).NotEmpty().WithName("محدوده اشتهار");

        When(x => x.Type == TypeEnum.ReligiousAuthorities || x.Type == TypeEnum.SpecialClergy, () => {
            RuleFor(x => x.Role).NotNull().WithMessage("فیلد عنوان اجباری است.");
        });
    }
}

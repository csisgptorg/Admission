using FluentValidation;
using Csis.Admission.Application.Features.Protests.Commands;

namespace Csis.Admission.Application.Features.Protests.Validators;

/// <summary>
/// اعتبارسنجی فرمان ثبت همسر از ثبت احوال
/// </summary>
public sealed class CreateProtestByCodmRequestCommandValidator : BaseValidator<CreateProtestByCodmRequestCommand>
{
    /// <summary> Ctor </summary>
    public CreateProtestByCodmRequestCommandValidator() {

        RuleFor(x => x.FieldId)
            .IsInEnum()
            .WithMessage("شناسه فیلد مورد اعتراض معتبر نیست.");

        When(x => x.FieldId != ProtestFormTitle.BeingLandlord && x.FieldId != ProtestFormTitle.HousingBuySellHistory && x.FieldId != ProtestFormTitle.PersonalHousingHistory, () => {
            RuleFor(x => x.FieldDescription)
                .NotEmpty()
                .WithMessage("شرح اعتراض الزامی است.")
                .MaximumLength(1000)
                .WithMessage("شرح اعتراض نمی‌تواند بیش از 1000 کاراکتر باشد.");


            RuleFor(x => x.Documents)
                .NotEmpty()
                .WithMessage("حداقل یک سند الزامی است.")
                .Must(docs => docs.Any(doc => doc.Type == DocumentType.FirstProtestDocument))
                .WithMessage("سند اول اجباری است.");

            RuleFor(x => x.HasHousingHistory)
                .Null()
                .WithMessage("این فیلد فقط برای اعتراضات مربوط به سوابق مسکن می‌باشد");
        });


        When(x => x.FieldId is ProtestFormTitle.BeingLandlord or ProtestFormTitle.HousingBuySellHistory or ProtestFormTitle.PersonalHousingHistory, () => {
            RuleFor(x => x.HasHousingHistory)
                .NotNull()
                .WithMessage("این فیلد برای اعتراضات مربوط به سوابق مسکن الزامی است.");
        });
    }
}

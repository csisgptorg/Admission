using Csis.Admission.Application.Features.ReligiousRoleQuestions.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.ReligiousRoleQuestions.Validators;
public sealed class CreateRequestReligiousRoleQuestionCommandValidator : BaseValidator<CreateRequestReligiousRoleQuestionCommand>
{
    public CreateRequestReligiousRoleQuestionCommandValidator() {
        RuleFor(x => x.ReligiouslyDressedDescription)
            .NotEmpty()
            .MaximumLength(2000)
            .WithName("توضیحات مرتبط با ملبس");
      
        When(x => x.HasRole, () => {
            RuleFor(x => x.ReligiousRoleType).IsInEnum().WithName("نوع نقش آفرینی");
            RuleFor(x => x.FileId).NotEmpty().WithName("مدرک نقش آفرینی");
            RuleFor(x => x.StudentReagentCodms).NotEmpty().WithName("کد های مرکز معرفین");
        });

        When(x => !x.HasRole, () => { 
            RuleFor(x => x.NotHavingRoleCause).IsInEnum().WithName("دلیل عدم فعالیت");
            RuleFor(x => x.HasRoleDescription).NotEmpty().MaximumLength(2000).WithName("توضیحات فعالیت حوزوی");
        });
    }
}

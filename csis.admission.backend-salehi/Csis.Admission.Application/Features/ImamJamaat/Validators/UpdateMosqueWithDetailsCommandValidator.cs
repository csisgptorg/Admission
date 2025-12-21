using Csis.Admission.Application.Features.ImamJamaat.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.ImamJamaat.Validators;

public class UpdateMosqueWithDetailsCommandValidator : AbstractValidator<UpdateMosqueWithDetailsCommand>
{
    public UpdateMosqueWithDetailsCommandValidator() {
        RuleFor(x => x.Mosque)
            .NotNull()
            .SetValidator(new MosqueDtoValidator());

        RuleFor(x => x.ImamJamaat)
            .NotNull()
            .SetValidator(new ImamJamaatDtoValidator());

        RuleFor(x => x.MosqueActivity)
            .NotNull()
            .SetValidator(new ActivityDtoValidator());
    }
}

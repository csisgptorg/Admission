using Csis.Admission.Application.Features.Researches.Commands;
using Csis.Admission.Application.Features.StudentFriends.Commands;
using FluentValidation;

namespace Csis.Admission.Application.Features.Researches.Validators;

public sealed class UpdateResearchCommandValidator : BaseValidator<UpdateResearchRequestCommand>
{
    public UpdateResearchCommandValidator() {
        RuleFor(x => x.ArticlePublication).MaximumLength(100).WithName("ArticlePublication");
        RuleFor(x => x.BookPublisher).MaximumLength(100).WithName("BookPublisher");
        RuleFor(x => x.BookShabak).MaximumLength(100).WithName("BookShabak");
        RuleFor(x => x.ProjectEmployer).MaximumLength(100).WithName("ProjectEmployer");
        RuleFor(x => x.Title).MaximumLength(100).WithName("Title");
    }
}

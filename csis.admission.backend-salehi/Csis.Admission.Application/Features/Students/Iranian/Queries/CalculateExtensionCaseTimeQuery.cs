using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <summary>
/// تمدید پرونده
/// </summary>
public sealed record CalculateExtensionCaseTimeQuery(int Codm) : IRequest<bool>;
internal sealed class CalculateExtensionCaseTimeQueryHandler(
    IRepository<StudentSummary> repo,
    IMediator mediator,
    ICsisAuthenticatedUserService authenticatedUserService)
    : IRequestHandler<CalculateExtensionCaseTimeQuery, bool>
{
    public async Task<bool> Handle(CalculateExtensionCaseTimeQuery request, CancellationToken cancellationToken) {
        var founded =
            await repo.GetOneAsync<StudentSummaryCaseDto>(x => x.Codm == request.Codm,
                cancellationToken: cancellationToken)
            ?? throw new CommandValidationException("پرونده ای با این مشخصات یافت نشد.");

        if ( founded.IsBlock ) {
            throw new CommandValidationException("پرونده شما مسدود می باشد. جهت رفع مسدودی با پشتیبانی تماس بگیرید.");
        }

        return founded.CaseValidityDate.StringDateToInt() >= DateTime.Now.AddMonths(3).ToPersianInteger();
    }
}

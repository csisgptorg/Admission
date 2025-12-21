using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.DependentCaseActive.Commands;

/// <summary>
/// ویرایش وضعیت غیرفعال بودن تکفل در پرونده پذیرش
/// </summary>
/// <param name="Codm"></param>
/// <param name="DependentId"></param>
/// <param name="DeActiveReason"></param>
/// <param name="ActiveReason"></param>
public sealed record class UpdateDependentCaseActiveStatusSeniorRequestCommand(int Codm, long DependentId, DependentDeActiveReasonEnum? DeActiveReason, DependentActiveReasonEnum? ActiveReason) : IRequest<long>;

internal sealed class UpdateDependentCaseDeActiveRequestHandler(IRepository<DependentSummary,long> studentRepository, IRequestService requestService) : IRequestHandler<UpdateDependentCaseActiveStatusSeniorRequestCommand, long>
{
    public async Task<long> Handle(UpdateDependentCaseActiveStatusSeniorRequestCommand request, CancellationToken cancellationToken) {
        var dependent = (await studentRepository.GetOneAsync(x => x.Id == request.DependentId))
            ?? throw new CommandValidationException("تکفل مورد نظر یافت نشد");
        if ( request.DeActiveReason.HasValue ) {

            if ( dependent.IsDead ) {
                throw new CommandValidationException("وضعیت تکفل در پرونده فوت شده می باشد و امکان ویرایش وجود ندارد");
            }
        }

        if ( request.ActiveReason.HasValue ) {

            if ( dependent.IsDead ) {
                throw new CommandValidationException("وضعیت تکفل در پرونده فوت شده می باشد و امکان ویرایش وجود ندارد");
            }
        }

        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.UpdateDependentCaseDeActiveSenior);

        var result = await requestService.Create(requestCommand, cancellationToken);
        return result;
    }
}

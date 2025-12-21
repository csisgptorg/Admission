using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.BlockedServices.Commands;

/// <summary>
/// CreateStudentCaseBlockRequest
/// </summary>
/// <param name="Codm"></param>
/// <param name="CaseBlockReasonId"></param>
public sealed record CreateStudentCaseBlockRequestCommand(int Codm, List<CaseBlockReason> CaseBlockReasonId) : IRequest<long>;
internal sealed class CreateStudentCaseBlockRequestHandler(IRequestService requestService,IRepository<StudentSummary> repository) : IRequestHandler<CreateStudentCaseBlockRequestCommand, long>
{
    public async Task<long> Handle(CreateStudentCaseBlockRequestCommand request, CancellationToken cancellationToken) {
        var student = await repository.GetOneAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        if ( student != null && student.IsBlock ) {
            throw new CommandValidationException($"پرونده با کد مرکز  {request.Codm} مسدود می باشد.");
        }

        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.CreateStudentCaseBlock);
        var result = await requestService.Create(requestCommand, cancellationToken);
        return result;
    }
}

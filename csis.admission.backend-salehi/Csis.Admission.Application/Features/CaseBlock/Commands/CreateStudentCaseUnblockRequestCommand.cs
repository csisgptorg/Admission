using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.BlockedServices.Commands;

/// <summary>
/// CreateStudentCaseUnblockRequestCommand
/// </summary>
/// <param name="Codm"></param>
public sealed record CreateStudentCaseUnblockRequestCommand(int Codm) : IRequest<long>;
internal sealed class CreateStudentCaseUnblockRequestCommandHandler(IRequestService requestService, IRepository<StudentSummary> repository) : IRequestHandler<CreateStudentCaseUnblockRequestCommand, long>
{
    public async Task<long> Handle(CreateStudentCaseUnblockRequestCommand request, CancellationToken cancellationToken) {
        var student = await repository.GetOneAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        if ( student != null && !student.IsBlock ) {
            throw new CommandValidationException($"پرونده با کد مرکز  {request.Codm} مسدود نمی باشد.");
        }

        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.CreateStudentCaseUnblock);
        var result = await requestService.Create(requestCommand, cancellationToken);
        return result;
    }
}

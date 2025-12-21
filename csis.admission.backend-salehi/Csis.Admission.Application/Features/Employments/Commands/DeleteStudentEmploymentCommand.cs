namespace Csis.Admission.Application.Features.Employments.Commands;

/// <summary>
/// Õ–› «‘ €«· ÿ·»Â
/// </summary>
/// <param name="Codm">òœ „—ò“ Œœ„« </param>
/// <param name="Id">‘‰«”Â «‘ €«·</param>
public sealed record DeleteStudentEmploymentCommand(int Codm, int Id) : IRequest<int>;

internal sealed class DeleteStudentEmploymentCommandHandler(
    IRepository<StudentEmployment> studentEmploymentRepository,
    ILogger<DeleteStudentEmploymentCommandHandler> logger)
    : IRequestHandler<DeleteStudentEmploymentCommand, int>
{
    public async Task<int> Handle(DeleteStudentEmploymentCommand request, CancellationToken cancellationToken)
    {
        await studentEmploymentRepository.DeleteAsync(request.Id, cancellationToken: cancellationToken);
        return request.Id;
    }
}

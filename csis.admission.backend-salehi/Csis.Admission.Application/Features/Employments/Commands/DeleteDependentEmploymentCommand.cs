namespace Csis.Admission.Application.Features.Employments.Commands;

/// <summary>
/// Õ–› «‘ €«·  ò›·
/// </summary>
/// <param name="Codm">òœ „—ò“ Œœ„« </param>
/// <param name="Id">‘‰«”Â «‘ €«·</param>
/// <param name="DependentId">‘‰«”Â  ò›·</param>
public sealed record DeleteDependentEmploymentCommand(int Codm, int Id, long DependentId) : IRequest<int>;

internal sealed class DeleteDependentEmploymentCommandHandler(
    IRepository<DependentEmployment> dependentEmploymentRepository, 
    ILogger<DeleteDependentEmploymentCommandHandler> logger) 
    : IRequestHandler<DeleteDependentEmploymentCommand, int>
{
    public async Task<int> Handle(DeleteDependentEmploymentCommand request, CancellationToken cancellationToken) {
     await dependentEmploymentRepository.DeleteAsync(request.Id, cancellationToken: cancellationToken);
     return request.Id;
    }
}

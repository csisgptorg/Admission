using Csis.Admission.Application.Features.Famouses.Dtos;

namespace Csis.Admission.Application.Features.Famouses.Queries;

/// <summary>
/// دریافت مشهور با شناسه
/// </summary>
/// <param name="Id">شناسه مشهور</param>
public sealed record GetFamousByIdQuery(int Id) : IRequest<StudentFamousDto>;

internal sealed class GetFamousByIdQueryHandler(IRepository<Famous> famousRepo)
    : IRequestHandler<GetFamousByIdQuery, StudentFamousDto>
{
    public async Task<StudentFamousDto> Handle(GetFamousByIdQuery request, CancellationToken cancellationToken) {
        return await famousRepo.GetByIdAsync<StudentFamousDto>(request.Id, cancellationToken: cancellationToken)
            ?? throw new CommandValidationException( $"طلبه مشهور با شناسه {request.Id} یافت نشد." );
    }
}

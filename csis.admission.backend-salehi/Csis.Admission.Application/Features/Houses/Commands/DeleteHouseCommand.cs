namespace Csis.Admission.Application.Features.Houses.Commands;

/// <summary>
/// Õ–› «ÿ·«⁄«  „”ò‰
/// </summary>
/// <param name="Codm">òœ „—ò“ Œœ„« </param>
/// <param name="Id">‘‰«”Â „”ò‰</param>
public sealed record DeleteHouseCommand(int Codm, int Id) : IRequest<int>;

internal sealed class DeleteHouseCommandHandler(
    IRepository<House> houseRepository,
    ILogger<DeleteHouseCommandHandler> logger)
    : IRequestHandler<DeleteHouseCommand, int>
{
    public async Task<int> Handle(DeleteHouseCommand request, CancellationToken cancellationToken)
    {
        await houseRepository.DeleteAsync(request.Id, cancellationToken: cancellationToken);
        return request.Id;
    }
}

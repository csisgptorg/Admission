using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Famouses.Commands;

/// <summary>
/// ویرایش مشهور
/// </summary>
public sealed record UpdateFamousCommand : BaseCommandDto<UpdateFamousCommand, Famous>, IRequest<int>
{
    /// <summary>
    /// شناسه مشهور
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// کد مرکز خدمات
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// محدوده
    /// </summary>
    public AreaEnum Area { get; init; }

    /// <summary>
    /// نقش
    /// </summary>
    public RoleEnum? Role { get; init; }

    /// <summary>
    /// نوع
    /// </summary>
    public TypeEnum Type { get; init; }
}

internal sealed class UpdateFamousCommandHandler(IRepository<Famous> famousRepo, ILogger<UpdateFamousCommandHandler> logger) : IRequestHandler<UpdateFamousCommand,int>
{
    public async Task<int> Handle(UpdateFamousCommand request, CancellationToken cancellationToken) {
        var famous = await famousRepo.GetByIdAsTrackingAsync(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<Famous>(request.Id);

        logger.LogDebug("Famous with id {id} before update: {@before}", request.Id, famous);

        famous = request.ToEntity(famous);

        logger.LogDebug("Famous with id {id} after update: {@after}", request.Id, famous);

        await famousRepo.UpdateAsync(famous, cancellationToken: cancellationToken);
        return famous.Id;
    }
}

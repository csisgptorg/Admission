using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Famouses.Commands;

/// <summary>
/// ایجاد مشهور جدید
/// </summary>
public sealed record CreateFamousCommand : BaseCommandDto<CreateFamousCommand, Famous>, IRequest<int>
{
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

internal sealed class CreateFamousCommandHandler(IRepository<Famous> famousRepo, ILogger<CreateFamousCommandHandler> logger) : IRequestHandler<CreateFamousCommand, int>
{
    public async Task<int> Handle(CreateFamousCommand request, CancellationToken cancellationToken) {
        var existingFamous = await famousRepo.GetOneAsTrackingAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken);

        var newFamous = new Famous();
        if ( existingFamous == null ) {
            newFamous = request.ToEntity();
            await famousRepo.InsertAsync(newFamous, cancellationToken: cancellationToken);
        } else {
            newFamous = request.ToEntity(existingFamous);
            await famousRepo.UpdateAsync(newFamous, cancellationToken: cancellationToken);
        }

        logger.LogDebug("Famous created with id {id}", newFamous.Id);
        return newFamous.Id;
    }
}

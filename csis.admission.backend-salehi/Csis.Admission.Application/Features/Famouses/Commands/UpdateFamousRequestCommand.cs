using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Famouses.Commands;

/// <summary>
/// ویرایش مشهور
/// </summary>
public sealed record UpdateFamousRequestCommand : BaseCommandDto<UpdateFamousRequestCommand, Famous>, IRequest
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
internal sealed class UpdateFamousRequestCommandHandler(IRequestService requestService, ILogger<UpdateFamousRequestCommandHandler> logger) : IRequestHandler<UpdateFamousRequestCommand>
{
    public async Task Handle(UpdateFamousRequestCommand request, CancellationToken cancellationToken) {
        var requestCommand = new CreateRequestCommand(request,RequestFlow.DirectRegistration,RequestType.UpdateFamous);
        logger.LogDebug("Creating update famous request: {@requestCommand}", requestCommand);
         await requestService.Create(requestCommand, cancellationToken);
    }
}

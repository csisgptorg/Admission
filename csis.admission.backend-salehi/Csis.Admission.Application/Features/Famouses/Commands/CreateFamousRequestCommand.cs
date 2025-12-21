using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Famouses.Commands;

/// <summary>
/// ایجاد مشهور جدید
/// </summary>
public sealed record CreateFamousRequestCommand : BaseCommandDto<CreateFamousRequestCommand, Famous>, IRequest<long>
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

internal sealed class CreateFamousRequestCommandHandler(IRequestService requestService, ILogger<CreateFamousRequestCommandHandler> logger) : IRequestHandler<CreateFamousRequestCommand, long>
{
    public async Task<long> Handle(CreateFamousRequestCommand request, CancellationToken cancellationToken) {
        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.CreateFamous);
       var result = await requestService.Create(requestCommand, cancellationToken);
       return result;
    }
}

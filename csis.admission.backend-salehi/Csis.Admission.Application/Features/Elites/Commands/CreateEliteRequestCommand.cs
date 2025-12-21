using Csis.Authorization.Services;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Elites.Commands;

/// <summary>
/// ثبت نخبگان (درخواست)
/// </summary>
public sealed record CreateEliteRequestCommand : IRequest<long>
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>نوع نخبگی</summary>
    public short? EliteTypeId { get; init; }

    /// <summary>سطح نخبگی</summary>
    public short? EliteLevelId { get; init; }

    /// <summary>تاریخ شروع</summary>
    public string? StartDate { get; init; }

    /// <summary>تاریخ پایان</summary>
    public string? EndDate { get; init; }

    /// <summary>مرجع تایید</summary>
    public string ApprovalCenterTitle { get; init; }
}

internal sealed class CreateEliteRequestCommandHandler(
    IRequestService requestService,
    ICurrentUserService currentUser
    ) : IRequestHandler<CreateEliteRequestCommand, long>
{
    public async Task<long> Handle(CreateEliteRequestCommand command, CancellationToken cancellationToken) {
        _ = await Common.Utilities.SetCodm(command, currentUser);

        var flow = RequestFlow.DirectRegistration;

        var requestCommand = new CreateRequestCommand(command, flow, RequestType.CreateOrUpdateElite);

        return await requestService.Create(requestCommand, cancellationToken);
    }
}

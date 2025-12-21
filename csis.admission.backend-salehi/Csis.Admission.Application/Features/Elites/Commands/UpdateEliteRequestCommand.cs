using Csis.Authorization.Services;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Elites.Commands;

/// <summary>
/// بروزرسان? نخبگان (درخواست)
/// </summary>
public sealed record UpdateEliteRequestCommand : IRequest<long>
{
    /// <summary>شناسه</summary>
    public int Id { get; set; }

    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>نوع نخبگ?</summary>
    public short? EliteTypeId { get; init; }

    /// <summary>سطح نخبگ?</summary>
    public short? EliteLevelId { get; init; }

    /// <summary>تار?خ شروع</summary>
    public string? StartDate { get; init; }

    /// <summary>تار?خ پا?ان</summary>
    public string? EndDate { get; init; }

    /// <summary>مرجع تا??د</summary>
    public string ApprovalCenterTitle { get; init; }
}

internal sealed class UpdateEliteRequestCommandHandler(
    IRequestService requestService,
    IRepository<Elite> repo,
    ICurrentUserService currentUser
 ) : IRequestHandler<UpdateEliteRequestCommand, long>
{
    public async Task<long> Handle(UpdateEliteRequestCommand command, CancellationToken cancellationToken) {
        _ = await Common.Utilities.SetCodm(command, currentUser);

        // Validation
        var elite = await repo.GetByIdAsync(command.Id, cancellationToken: cancellationToken);
        if ( elite == null ) {
            throw new CommandValidationException($"نخبه با شناسه {command.Id} یافت نشد");
        }

        var flow = RequestFlow.DirectRegistration;

        var requestCommand = new CreateRequestCommand(command, flow, RequestType.CreateOrUpdateElite);
        return await requestService.Create(requestCommand, cancellationToken);
    }
}

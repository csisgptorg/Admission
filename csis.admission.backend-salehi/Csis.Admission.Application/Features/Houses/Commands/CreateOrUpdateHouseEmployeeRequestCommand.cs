using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Application.Features.Houses.Dtos;

namespace Csis.Admission.Application.Features.Houses.Commands;

/// <summary>
/// ایجاد درخواست مسکن طلبه
/// </summary>
public sealed record CreateOrUpdateHouseEmployeeRequestCommand : BaseCommandDto<CreateOrUpdateHouseEmployeeRequestCommand, House>, IRequest
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// وضعیت سکونت (شخصی، حمایتی، اجاره‌ای/رهنی)
    /// </summary>
    public HouseStatus HouseStatus { get; init; }

    /// <summary>
    /// جزئیات وضعیت سکونت (سازمانی، پدری، منزل همسر، سایر)
    /// </summary>
    public HouseStatusItem? HouseStatusItem { get; init; }

    /// <summary>
    /// توضیح جزئیات وضعیت سکونت (وقتی سایر انتخاب شود)
    /// </summary>
    public string HouseStatusItemDesc { get; init; }

    /// <summary>
    /// آیا دارای خانه شخصی می‌باشید؟
    /// </summary>
    public bool? HasHouse { get; init; }

    /// <summary>
    /// آیا دارای زمین شخصی می‌باشید؟
    /// </summary>
    public bool? HasLand { get; init; }

    /// <summary>
    /// آیا در حجره یا خوابگاه نیز سکونت دارید؟
    /// </summary>
    public bool? LiveInCell { get; init; }

    /// <summary>
    /// مسکن اجاره ای
    /// </summary>
    public TenantDto? Tenant { get; set; }

    /// <summary>
    /// لیست فایل‌ها (مدارک دیگر)
    /// </summary>
    public RequestDocumentDto[] Documents { get; init; } = [];

    /// <summary>تایید</summary>
    public bool? Confirmed { get; set; }
}

internal sealed class CreateOrUpdateHouseEmployeeRequestCommandHandler(
    IRepository<House> houseRepo,
    IRepository<Tenant> tenantRepo,
    IRequestService requestService,
    ICurrentUserService currentUser)
    : IRequestHandler<CreateOrUpdateHouseEmployeeRequestCommand>
{
    public async Task Handle(CreateOrUpdateHouseEmployeeRequestCommand command, CancellationToken cancellationToken)
    {
        var house = await houseRepo.GetOneAsTrackingAsync(x => x.Codm == command.Codm, cancellationToken: cancellationToken);

        // اطمینان از همخوانی Codm در Tenant
        if (command.Tenant != null)
        {
            command.Tenant = command.Tenant with { Codm = command.Codm };
        }

        var flow = await GetRequestFlow(command, house);
        var requestCommand = new CreateRequestCommand(command, flow, RequestType.CreateOrUpdateHouse) { PayloadModel = "CreateOrUpdateHouseCommand" };
        requestCommand.Documents = command.Documents;
        await requestService.Create(requestCommand, cancellationToken);
    }

    /// <summary>فلو</summary>
    private async Task<RequestFlow> GetRequestFlow(CreateOrUpdateHouseEmployeeRequestCommand request, House house)
    {
        var fileUploadTypes = request.Documents?.Select(d => d.Type).Distinct().ToList() ?? [];
        var isStudent = await currentUser.IsStudent();
        var isEmployee = await currentUser.IsEmployee();
        var isSeniorPersonnel = await currentUser.IsSenior();
        return await RequestFlowHelper.DetermineRequestFlowAsync(request.HouseStatus, house?.HouseStatus, isStudent, isEmployee, isSeniorPersonnel, fileUploadTypes);
    }
}

using Csis.Authorization.Services;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Veterans.Commands;

/// <summary>
/// ثبت یا بروزرسانی اطلاعات ایثارگری (درخواست)
/// </summary>
public sealed record CreateOrUpdateVeteranRequestCommand : IRequest<long>
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>تعداد روز دفاع از حرم</summary>
    public int? HaramDefenceDays { get; init; }

    /// <summary>تعداد روز دفاع مقدس</summary>
    public int? HolyDefenseDays { get; init; }

    /// <summary>تعداد روز آزادگی</summary>
    public int? CaptivityDays { get; init; }

    /// <summary>تعداد روز زندان قبل از انقلاب</summary>
    public int? JailDays { get; init; }

    /// <summary>تعداد روز تبعید قبل از انقلاب</summary>
    public int? ExileDays { get; init; }

    /// <summary>درصد جانبازی</summary>
    public short? VeteranPercent { get; init; }

    /// <summary>نسبت با شهید</summary>
    public DependentRelation? RelationWithMartyr { get; init; }

    /// <summary>نوع شهادت</summary>
    public MartyrType? MartyrType { get; init; }

}

internal sealed class CreateOrUpdateVeteranRequestCommandHandler(
    IRequestService requestService,
    IRepository<Veteran> repo,
    ICsisAuthenticatedUserService authenticatedUser
    ) : IRequestHandler<CreateOrUpdateVeteranRequestCommand, long>
{
    public async Task<long> Handle(CreateOrUpdateVeteranRequestCommand command, CancellationToken cancellationToken)
    {
        var requestCommand = new CreateRequestCommand(command, RequestFlow.DirectRegistration, RequestType.CreateOrUpdateVeteran);
        return await requestService.Create(requestCommand, cancellationToken);
    }
}

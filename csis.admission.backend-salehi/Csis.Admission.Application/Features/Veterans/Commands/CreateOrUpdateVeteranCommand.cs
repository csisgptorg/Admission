using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Veterans.Commands;

/// <summary>
/// ثبت یا بروزرسانی اطلاعات ایثارگری
/// </summary>
public sealed record CreateOrUpdateVeteranCommand : BaseCommandDto<CreateOrUpdateVeteranCommand, Veteran>, IRequest<int>
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; init; }

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

    /// <summary>شناسه درخواست</summary>
    public long? RequestId { get; init; }
}

internal sealed class CreateOrUpdateVeteranCommandHandler(IRepository<Veteran> repo)
    : IRequestHandler<CreateOrUpdateVeteranCommand, int>
{
    public async Task<int> Handle(CreateOrUpdateVeteranCommand command, CancellationToken cancellationToken) {
        var veteran = await repo.GetOneAsTrackingAsync(x => x.Codm == command.Codm, cancellationToken: cancellationToken);
        Veteran newVeteran;
        if ( veteran is null ) {
            newVeteran = command.ToEntity();
            await repo.InsertAsync(newVeteran, cancellationToken: cancellationToken);
        } else {
            newVeteran = command.ToEntity(veteran);
            await repo.UpdateAsync(newVeteran, cancellationToken: cancellationToken);
        }

        return newVeteran.Id;
    }
}

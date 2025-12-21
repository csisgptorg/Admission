using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Elites.Commands;

/// <summary>
/// ثبت نخبگان
/// </summary>
public sealed record CreateEliteCommand : BaseCommandDto<CreateEliteCommand, Elite>, IRequest<int>
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>نوع نخبگی</summary>
    public short? EliteTypeId { get; set; }

    /// <summary>سطح نخبگی</summary>
    public short? EliteLevelId { get; set; }

    /// <summary>تاریخ شروع</summary>
    public string? StartDate { get; set; }

    /// <summary>تاریخ پایان</summary>
    public string? EndDate { get; set; }

    /// <summary>مرجع تاید</summary>
    public string ApprovalCenterTitle { get; set; }

    /// <summary>شناسه درخواست</summary>
    public long? RequestId { get; set; }

    /// <summary>
    /// تنظیم نگاشت‌های سفارشی
    /// </summary>
    /// <param name="mapping"></param>
    public override void ReverseCustomMappings(IMappingExpression<CreateEliteCommand, Elite> mapping) {
        base.ReverseCustomMappings(mapping);
        mapping.ForMember(dest => dest.StartDate, cfg => cfg.MapFrom(src => src.StartDate.StringDateToInt()));
        mapping.ForMember(dest => dest.EndDate, cfg => cfg.MapFrom(src => src.EndDate.StringDateToInt()));
    }
}

internal sealed class CreateEliteCommandHandler(IRepository<Elite> repo)
    : IRequestHandler<CreateEliteCommand, int>
{
    public async Task<int> Handle(CreateEliteCommand command, CancellationToken cancellationToken) {
        var elite = command.ToEntity();
    await repo.InsertAsync(elite, cancellationToken: cancellationToken);
        return elite.Id;
    }
}

using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Preaches.Commands;

/// <summary>
/// UpdatePreachCommand
/// </summary>
public sealed record UpdatePreachCommand : BaseCommandDto<UpdatePreachCommand, Preach>, IRequest<long>
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// کشور
    /// </summary>
    public int? CountryId { get; set; }

    /// <summary>
    /// استان
    /// </summary>
    public short? ProvinceId { get; set; }

    /// <summary>
    /// شهر
    /// </summary>
    public int? CityId { get; set; }

    /// <summary>
    /// تاریخ شروع
    /// </summary>
    public string StartDate { get; set; }

    /// <summary>
    /// تاریخ پایان
    /// </summary>
    public string EndDate { get; set; }

    /// <summary>
    /// نوع تبلیغ
    /// </summary>
    public PreachKind? Kind { get; set; }

    /// <summary>
    /// محل صدور مدرک
    /// </summary>
    public PreachApprovalCenter? ApprovalCenter { get; set; }

    /// <summary>
    /// شناسه تبلیغ در مرکز حوزوی
    /// </summary>
    public string RecordIdInApprovalCenter { get; set; }

    /// <summary>
    /// CustomMappings
    /// </summary>
    /// <param name="mapping"></param>
    public override void ReverseCustomMappings(IMappingExpression<UpdatePreachCommand, Preach> mapping) {
        mapping.ForMember(model => model.StartDate, config => config.MapFrom(dto => dto.StartDate.StringDateToInt()));
        mapping.ForMember(model => model.EndDate, config => config.MapFrom(dto => dto.EndDate.StringDateToInt()));
    }
}

internal sealed class UpdatePreachCommandHandler(IRepository<Preach> preachRepo) : IRequestHandler<UpdatePreachCommand, long>
{
    public async Task<long> Handle(UpdatePreachCommand request, CancellationToken cancellationToken) {
        var preach = await preachRepo.GetByIdAsTrackingAsync(request.Id, cancellationToken: cancellationToken)
            ?? throw new CommandValidationException($"تبلیغ با شناسه {request.Id} یافت نشد.");

        request.ToEntity(preach);
        await preachRepo.UpdateAsync(preach, true, cancellationToken);
        return preach.Id;
    }
}

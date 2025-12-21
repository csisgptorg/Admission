using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Preaches.Commands;

/// <summary>ثبت تبلیغ</summary>
public sealed record CreatePreachRequestCommand : BaseCommandDto<CreatePreachRequestCommand, Preach>, IRequest<long>
{
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
    public override void ReverseCustomMappings(IMappingExpression<CreatePreachRequestCommand, Preach> mapping) {
        mapping.ForMember(model => model.StartDate, config => config.MapFrom(dto => dto.StartDate.StringDateToInt()));
        mapping.ForMember(model => model.EndDate, config => config.MapFrom(dto => dto.EndDate.StringDateToInt()));
    }
}


internal sealed class CreatePreachRequestCommandHandler(IRequestService requestService)
    : IRequestHandler<CreatePreachRequestCommand, long>
{
    public async Task<long> Handle(CreatePreachRequestCommand request, CancellationToken cancellationToken) {
        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.CreatePreach);
        var result = await requestService.Create(requestCommand, cancellationToken);
        return result;
    }
}

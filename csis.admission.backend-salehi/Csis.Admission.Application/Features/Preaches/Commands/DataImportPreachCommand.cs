using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Preaches.Commands;

/// <summary>ثبت تبلیغ</summary>
public sealed record DataImportPreachCommand : BaseCommandDto<DataImportPreachCommand, Preach>, IRequest<int>
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
    public string City { get; set; }

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
    public override void ReverseCustomMappings(IMappingExpression<DataImportPreachCommand, Preach> mapping) {
        mapping.ForMember(model => model.StartDate, config => config.MapFrom(dto => dto.StartDate.StringDateToInt()));
        mapping.ForMember(model => model.EndDate, config => config.MapFrom(dto => dto.EndDate.StringDateToInt()));
    }
}

internal sealed class DataImportPreachCommandHandler : IRequestHandler<DataImportPreachCommand, int>
{
    private readonly IRepository<Preach> _preachRepo;
    public DataImportPreachCommandHandler(IRepository<Preach> preachRepo) {
        _preachRepo = preachRepo;
    }

    public async Task<int> Handle(DataImportPreachCommand command, CancellationToken cancellationToken) {
        var preachRequest = command;

        var preachList = await _preachRepo.GetAllAsTrackingAsync(i => i.ApprovalCenter == preachRequest.ApprovalCenter && i.RecordIdInApprovalCenter == preachRequest.RecordIdInApprovalCenter, cancellationToken: cancellationToken);

        if ( preachList.Count > 1 ) {

            throw new CommandValidationException("برای این آی دی مرکز حوزوی بیش از یک رکورد ثبت شده است");
        }

        if ( preachList.Count == 1 && preachList.Exists(i => i.Codm != preachRequest.Codm) ) {

            throw new CommandValidationException(" این آی دی مرکز حوزوی برای کد مرکز دیگری ثبت شده است");
        }

        if ( preachList.Count == 1 && preachList.Exists(i => i.Codm == preachRequest.Codm && i.EndDate == preachRequest.EndDate.Replace("/","").Replace("-","").ToInt()) ) {

            throw new CommandValidationException("  آی دی مرکز حوزوی تکراری می باشد");
        }

        if ( preachList.Count == 1
            && preachList.Exists(i => i.Codm == preachRequest.Codm && (i.EndDate == null || i.EndDate < preachRequest.EndDate.Replace("/", "").Replace("-", "").ToInt())) ) {

            var newPreach = preachList.Single();
            newPreach.EndDate = preachRequest.EndDate.Replace("/", "").Replace("-", "").ToInt();
            await _preachRepo.UpdateAsync(newPreach, true, cancellationToken);

            return newPreach.Id;

        }

        preachList = null;

        preachList = await _preachRepo.GetAllAsync(i => i.Codm == preachRequest.Codm, cancellationToken: cancellationToken);




        var similarList = preachList.Where(i => i.ApprovalCenter == preachRequest.ApprovalCenter
                                            && i.Kind == preachRequest.Kind
                                            && i.ProvinceId == preachRequest.ProvinceId
                                            && i.City.Replace('ي', 'ی').Replace('ك', 'ک').Replace(" ", "") == preachRequest.City.Replace('ي', 'ی').Replace('ك', 'ک').Replace(" ", "")
                                            && i.StartDate == preachRequest.StartDate.Replace("/", "").Replace("-", "").ToInt()
                                            && (i.EndDate == null || i.EndDate < preachRequest.EndDate.Replace("/", "").Replace("-", "").ToInt())
                                            && i.RecordIdInApprovalCenter == null
                                            );

        if ( similarList.Count() > 1 ) {
            throw new CommandValidationException("بیش از یک رکورد برای بروزرسانی تاریخ پایان یافت شد");
        }


        if ( similarList.Count() == 1 ) {
            var newPreach = similarList.Single();
            newPreach.EndDate = preachRequest.EndDate.Replace("/", "").Replace("-", "").ToInt();
            await _preachRepo.UpdateAsync(newPreach, true, cancellationToken);
            return newPreach.Id;

        }



        if ( preachList.Exists(i => i.ApprovalCenter == preachRequest.ApprovalCenter
                                    && i.Kind == preachRequest.Kind
                                    && i.StartDate == preachRequest.StartDate.Replace("/", "").Replace("-", "").ToInt()) ) {

            throw new CommandValidationException("تکراری");
        }

        var preachEntity = preachRequest.ToEntity();
        await _preachRepo.InsertAsync(preachEntity, cancellationToken: cancellationToken);
        return preachEntity.Id;
    }
}

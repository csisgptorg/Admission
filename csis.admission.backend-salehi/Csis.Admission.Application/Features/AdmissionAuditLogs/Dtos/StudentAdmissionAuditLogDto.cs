using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.AdmissionAuditLogs.Dtos;

/// <summary>سوابق اطلاعات پذیریش طلبه</summary>
public record StudentAdmissionAuditLogDto : BaseDto<StudentAdmissionAuditLogDto, StudentAdmissionAuditLog>
{
    /// <summary>نام کامل کاربر</summary>
    public string User { get; set; }

    /// <summary>طلبه</summary>
    public string Student { get; set; }

    /// <summary>جدول</summary>
    public string Table { get; set; }

    /// <summary>ستون</summary>
    public string Field { get; set; }

    /// <summary>شناسه جدول</summary>
    public int? TableRecordId { get; set; }

    /// <summary>مقدار قدیم</summary>
    public string OldValueDisplay { get; set; }

    /// <summary>مقدار جدید</summary>
    public string NewValueDisplay { get; set; }

    /// <summary>تاریخ</summary>
    public string Date { get; set; }

    /// <summary>ساعت</summary>
    public string Time { get; set; }

    /// <summary>محل ثبت اطلاعات</summary>
    public string DataSource { get; set; }

    /// <summary>شناسه درخواست</summary>
    public string RequestId { get; set; }

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<StudentAdmissionAuditLog, StudentAdmissionAuditLogDto> mapping) {
        mapping.ForMember(dto => dto.User, config => config.MapFrom(model => model.UserFullName));
        mapping.ForMember(dto => dto.Student, config => config.MapFrom(model => model.StudentFullName));
        mapping.ForMember(dto => dto.Table, config => config.MapFrom(model => model.TableTitle));
        mapping.ForMember(dto => dto.Field, config => config.MapFrom(model => model.FieldTitle));
        mapping.ForMember(dto => dto.DataSource, config => config.MapFrom(model => model.DataSourceTitle));
        mapping.ForMember(dto => dto.Date, config => config.MapFrom(model => model.Date.IntDateToString()));
        mapping.ForMember(dto => dto.Time, config => config.MapFrom(model => model.Time.TimeToString()));
    }
}

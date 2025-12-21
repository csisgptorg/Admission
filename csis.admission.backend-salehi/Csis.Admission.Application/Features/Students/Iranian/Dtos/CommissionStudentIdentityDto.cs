using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Enums;

namespace Csis.Admission.Application.Features.Students.Dtos;

/// <summary>اطلاعات شناسنامه ای در کمسیون</summary>
public record CommissionStudentIdentityDto : BaseDto<CommissionStudentIdentityDto, AdmissionCaseUser, Guid>
{
    /// <summary>شناسه</summary>
    public int CommissionRequestId { get; set; }

    /// <summary>نام</summary>
    public string FirstName { get; set; }

    /// <summary>نام خانوادگی</summary>
    public string LastName { get; set; }

    /// <summary>نام پدر</summary>
    public string FatherName { get; set; }

    /// <summary>کد ملی</summary>
    public string NationalCode { get; set; }

    /// <summary>کد یکتا</summary>
    public string YektaCode { get; set; }

    /// <summary>تاریخ تولد</summary>
    public int BirthDate { get; set; }

    /// <summary>شماره شناسنامه</summary>
    public string BirthCertNumber { get; set; }

    /// <summary>توضیحات شناسنامه</summary>
    public string BirthCertDescription { get; set; }

    /// <summary>سری شناسنامه</summary>
    public string BirthCertSeri { get; set; }

    /// <summary>سریال شناسنامه</summary>
    public int? BirthCertSerial { get; set; }

    /// <summary>محل صدور شناسنامه</summary>
    public string BirthCertIssuePlace { get; set; }

    /// <summary>تابعیت</summary>
    public Citizenship Citizenship { get; set; }

    /// <summary>جنسیت</summary>
    public Gender Gender { get; set; }

    /// <summary>وضعیت</summary>
    public CommissionRequestStatus Status { get; set; }

    /// <summary>عنوان وضعیت</summary>
    public string StatusTitle { get; set; }

    /// <summary>موبایل</summary>
    public string Mobile { get; set; }

    /// <summary> مذهب </summary>
    public Religion Religion { get; set; }

    /// <inheritdoc/>
    public override void ReverseCustomMappings(IMappingExpression<CommissionStudentIdentityDto, AdmissionCaseUser> mapping) {
        mapping.ForMember(model => model.ApprovalCenter, config => config.MapFrom(dto => ApprovalCenter.Commission));
        mapping.ForMember(model => model.CaseNumInApprovalCenter, config => config.MapFrom(dto => dto.CommissionRequestId));
        mapping.ForMember(model => model.ConfirmIdentityInformation, config => config.MapFrom(dto => true));
        mapping.ForMember(model => model.ConfirmMobile, config => config.MapFrom(dto => true));
    }
}

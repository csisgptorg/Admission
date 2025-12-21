using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <inheritdoc/>
public class CsisWSMLog : SoftDeletedBaseEntity<long>
{
    /// <inheritdoc/>
    public CsisWSMLog(int codm, string nationalCode, ServiceEnum service, bool succeeded, string request, string response) {
        Codm = codm;
        NationalCode = nationalCode;
        Service = service;
        Succeeded = succeeded;
        Request = request;
        Response = response;
    }

    /// <inheritdoc/>
    public CsisWSMLog(string nationalCode, ServiceEnum service, bool succeeded, string request, string response) {
        NationalCode = nationalCode;
        Service = service;
        Succeeded = succeeded;
        Request = request;
        Response = response;
    }

    /// <inheritdoc/>
    public CsisWSMLog() { }

    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>کد ملی</summary>
    public string NationalCode { get; set; }

    /// <summary>کد یکتا</summary>
    public string YektaCode { get; set; }

    /// <summary>مرکز تایید کننده</summary>
    public string ApprovalCenter { get; set; }

    /// <summary>شماره پرونده در مرکز تایید کننده</summary>
    public string CaseNumberInApprovalCenter { get; set; }

    /// <summary>گروه دیتایی - برای اطلاعات دریافتی از ارتباط داده ای</summary>
    public string DataGroup { get; set; }

    /// <summary>شماره تلفن</summary>
    public string PhoneNumber { get; set; }

    /// <inheritdoc/>
    public ServiceEnum Service { get; set; }
    /// <inheritdoc/>
    public bool Succeeded { get; set; }
    /// <inheritdoc/>
    public string Request { get; set; }
    /// <inheritdoc/>
    public string Response { get; set; }

    /// <inheritdoc/>
    public enum ServiceEnum : short
    {
        /// <inheritdoc/>
        GetAddressByPostalCode = 1,

        /// <inheritdoc/>
        ValidateMobileOwnership = 2,

        /// <summary>استعلام رابطه زوجیت برای ازدواج</summary>
        ValidateSpousalMarriageRelationship = 3,

        /// <inheritdoc/>
        GetIdentityInfoByNationalCode = 4,

        /// <inheritdoc/>
        ValidateSibaAccountNumber = 5,

        /// <summary>دریافت اطلاعات حوزوی طلبه از ارتباط داده ای</summary>
        GetStudentInfoInMarakezHowzavi = 6,
        /// <summary> استعلام ارتباط داده ای برای بررسی رابطه والدین و فرزندان </summary>
        ValidateParentChildRelationship = 7,
        /// <summary>استعلام کد یکتا برای افراد غیر ایرانی</summary>
        ValidateNonIranianYektaCode = 8,

        /// <summary>استعلام رابطه زوجیت برای طلاق</summary>
        ValidateSpousalDivorceRelationship = 9,

        /// <summary>استعلام رابطه برای افراد غیر ایرانی</summary>
        ValidateNonIranianRelationship = 10,

        /// <summary>دریافت اطلاعات تحصیلی از وزرات علوم</summary>
        AcademicRecordsFromMinistry = 11,
        /// <summary>دریافت اطلاعات هویتی از طریق ApiM</summary>
        GetIdentityInfoByNationalCodeWsmApi = 12,

        /// <summary>استعلام رابطه برای افراد غیر ایرانی</summary>
        AlmostafaPersonHoviat = 13,

        /// <summary>استعلام شماره شبا برای افراد ایرانی</summary>
        ValidateShebaOwnership = 14,

        /// <summary>دریافت خلاصه تجمیعی</summary>
        GetTajmieiSummary = 15,
        /// <summary> دریافت تصویر فرد ایرانی از ثبت احوال</summary>
        GetIranianImageFromSabteAhval = 16,
    }

    //TODO : بعدا باید تغییر کند و Factory Method برای همه موارد در نظر گرفته شود

    //public static CsisWSMLog CreateWithNationalCode(string nationalCode, ServiceEnum service, bool succeeded, string request, string response)
    //    => new CsisWSMLog { NationalCode = nationalCode, Service = service, Succeeded = succeeded, Request = request, Response = response };

    /// <summary>
    /// ایجاد لاگ با کد یکتا
    /// </summary>
    /// <param name="yektaCode"></param>
    /// <param name="service"></param>
    /// <param name="succeeded"></param>
    /// <param name="request"></param>
    /// <param name="response"></param>
    /// <returns></returns>
    public static CsisWSMLog CreateWithYektaCode(int? codm, string yektaCode, ServiceEnum service, bool succeeded, string request, string response)
        => new CsisWSMLog { Codm = codm.Value, YektaCode = yektaCode, Service = service, Succeeded = succeeded, Request = request, Response = response };
}

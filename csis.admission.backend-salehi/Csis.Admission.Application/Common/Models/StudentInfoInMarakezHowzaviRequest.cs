namespace Csis.Admission.Application.Common.Models;

/// <summary>ورودی برای سرویس دریافت اطلاعات طلبه در مراکز حوزوی</summary>
public class StudentInfoInMarakezHowzaviRequest
{
    /// <summary>کد مرکز خدمات</summary>
    public string Codm { get; set; }

    /// <summary>مرکز تایید کننده</summary>
    public string ApprovalCenter { get; set; }

    /// <summary>شماره پرونده در مرکز تایید کننده</summary>
    public string CaseNumberInApprovalCenter { get; set; }

    /// <summary>کد ملی</summary>
    public string NationalCode { get; set; }

    /// <summary>کد یکتا</summary>
    public string YektaCode { get; set; }

    /// <summary>گروه دیتا</summary>
    public string DataGroup { get; set; }
}

/// <summary>خروجی برای سرویس دریافت اطلاعات طلبه در مراکز حوزوی</summary>
public class StudentInfoInMarakezHowzaviResponse
{
    /// <inheritdoc/>
    public string JsonData { get; set; }
    /// <inheritdoc/>
    public object Data { get; set; }
    /// <inheritdoc/>
    public List<object> Errors { get; set; }
    /// <inheritdoc/>
    public object ErrorDetails { get; set; }
    /// <inheritdoc/>
    public string Message { get; set; }
    /// <inheritdoc/>
    public bool IsSuccess { get; set; }
    /// <inheritdoc/>
    public int StatusCode { get; set; }
    /// <inheritdoc/>
    public bool IsExceptionThrown { get; set; }
}

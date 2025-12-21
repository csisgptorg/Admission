namespace Csis.Admission.Application.Enums;

/// <summary>اینام نوع دیتای ستون</summary>
public enum ColumnType
{
    /// <summary>رشته</summary>
    String,

    /// <summary>عدد</summary>
    Numeric,

    /// <summary>عدد اعشاری</summary>
    Decimal,

    /// <summary>تاریخ</summary>
    Date,

    /// <summary>زمان</summary>
    Time,

    /// <summary>بولین</summary>
    Boolean,

    /// <summary>لیست</summary>
    List,

    /// <summary>لیست اعداد</summary>
    NumericList
}

using Csis.Admission.Application.Enums;

namespace Csis.Admission.Application.Common;
/// <summary>اتریبیوت کوئری ساز</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property,AllowMultiple = false)]
public sealed class QueryBuilderAttribute : Attribute
{
    /// <summary>نام</summary>
    public string Name { get; set; }

    /// <summary>نام نمایشی</summary>
    public string Label { get; set; }

    /// <summary>جدول رابط</summary>
    public string RelationTable { get; set; }

    /// <summary>نوع</summary>
    public Type Type { get; set; }

    /// <summary>سورس</summary>
    public ColumnSourceType Source { get; set; }

    /// <summary>لیبل ستون وابسته</summary>
    public string DependentLabel { get; set; }

    /// <summary>ستون وابسته</summary>
    public string DependentColumn { get; set; }

    /// <summary>ای پی آی وابسته</summary>
    public string DependentApi { get; set; }

    /// <summary>تب</summary>
    public ReportBuilderTab Tab { get; set; }

    /// <summary>لیست اپراتورها</summary>
    public string[] Operators { get; set; }
}


using Csis.Admission.Application.Common.Configuration;

namespace Csis.Admission.Application.Features.ReportBuilders.Dtos;

/// <summary>گزاش ساز</summary>
public sealed record ReportBuilderQueryDto
{
    /// <inheritdoc/>
    public ReportBuilderQueryDto(object data, MetadataDto metadata, string query) {
        Data = data;
        Metadata = metadata;
        Query = GlobalOptions.IsDevelopment ? query: null;
    }

    /// <summary>پاسخ ناموفق</summary>
    public static ReportBuilderQueryDto FailedResponse(string query,string message) {
        return new ReportBuilderQueryDto(null, null, null) {
            Query= query,
            Failed = true,
            Message = message,
            Succeeded = false
        };
    }

    /// <summary>کوئری اجرا شده - فقط در حالت توسعه</summary>
    public string Query { get; private set; }

    /// <summary>وضعیت شکست عملیات</summary>
    public bool Failed { get; private set; } = false;

    /// <summary>پیام پاسخ</summary>
    public string Message { get; private set; }

    /// <summary>وضعیت موفقیت عملیات</summary>
    public bool Succeeded { get; private set; } = true;

    /// <summary>کد وضعیت پاسخ</summary>
    public int Code { get; } = 0;

    /// <summary>داده بازگشتی</summary>
    public object Data { get; }

    /// <summary>اطلاعات صفحه‌بندی</summary>
    public MetadataDto Metadata { get; }


    /// <summary>اطلاعات صفحه‌بندی</summary>
    public class MetadataDto
    {
        /// <inheritdoc/>
        public MetadataDto(int pageIndex, int pageSize, dynamic[] countResult) {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = (int) countResult.First().CountResult;
            TotalPages = (int) Math.Ceiling(TotalCount / (double) PageSize);
            HasPreviousPage = PageIndex > 1;
            HasNextPage = PageIndex < TotalPages;
        }

        /// <summary>شماره صفحه فعلی</summary>
        public int PageIndex { get; }
        /// <summary>تعداد آیتم در هر صفحه</summary>
        public int PageSize { get; }
        /// <summary>تعداد کل صفحات</summary>
        public int TotalPages { get; }
        /// <summary>تعداد کل آیتم‌ها</summary>
        public int TotalCount { get; }
        /// <summary>آیا صفحه قبلی وجود دارد؟</summary>
        public bool HasPreviousPage { get; }
        /// <summary>آیا صفحه بعدی وجود دارد؟</summary>
        public bool HasNextPage { get; }
    }
}

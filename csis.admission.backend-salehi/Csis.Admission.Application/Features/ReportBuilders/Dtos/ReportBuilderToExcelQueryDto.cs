namespace Csis.Admission.Application.Features.ReportBuilders.Dtos;

/// <summary>خروجی اکسل گزارش ساز</summary>
public sealed record ReportBuilderToExcelQueryDto(string FileName, byte[] FileByte,
    string MIMEType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

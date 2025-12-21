namespace Csis.Admission.Application.Common.Models;

/// <summary>دریافت تحصیلات دانشگاهی</summary>
public record InquiryCertificateModel(int Codm,long? DependentId, string NationalCode, string TraceCode);

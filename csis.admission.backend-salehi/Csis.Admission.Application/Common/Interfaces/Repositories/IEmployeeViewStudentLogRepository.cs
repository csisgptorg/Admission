
using Csis.Admission.Application.Features.ViewLogs.Dtos;

namespace Csis.Admission.Application.Common.Interfaces.Repositories;

/// <summary>ریپو تاریخچه طلاب مشاهد شده توسط کارمند</summary>
public partial interface IEmployeeViewStudentLogRepository
{
    /// <summary>آخرین تارچه مشاهدات</summary>
    Task<EmployeeViewStudentLogDto[]> GetLastLogs(CancellationToken cancellationToken);
}

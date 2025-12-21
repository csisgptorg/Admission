namespace Csis.Admission.Application.Features.Students.Dtos;

/// <summary>دریافت وضعیت تحصیلی طلبه</summary>
public record GetStudentEducationStatusDto
{
    /// <summary>وضعیت تحصیلی</summary>
    public EducationStatus EducationStatus { get; set; }

}

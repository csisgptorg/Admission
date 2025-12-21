using Csis.Admission.Application.Features.Students.Dtos;

namespace Csis.Admission.Application.Common.Interfaces.Repositories;

/// <summary>ریپو خلاصه اطلاعات طلبه</summary>
public interface IStudentSummaryRepository
{
    /// <summary>کارت الکترونیکی طلبه</summary>
    Task<StudentECardDto> GetStudentElectronicCardByCodm(int codm);
}

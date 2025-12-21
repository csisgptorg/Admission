using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models.Repository;

namespace Csis.Admission.Application.Common.Interfaces.Repositories.Student;

/// <inheritdoc/>
public interface IStudentDependentRepository
{
    /// <inheritdoc/>
    Task<ProcedureResultDto> Create(StudentDependentRegistryPrcRequest command);

    /// <summary>
    /// ثبت  تاریخ ازدواج برای طلبه خواهر
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<ProcedureResultDto> CreateSisterStudentMarriageAsync(SisterStudentMarriagePrcRequest command);



    /// <summary>
    /// ثبت  تاریخ ازدواج  افراد تحت تکفل - فرزندان - ازدواج ثبت و پرونده بسته می شود
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<ProcedureResultDto> UpdateDependentChildMarriageAsync(UpdateDependentMarriagePrcRequest command);

    /// <summary>
    /// ثبت  تاریخ ازدواج افراد تحت تکفل - همسران بیوه - فقط پرونده بسته می شود
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<ProcedureResultDto> UpdateDependentSpouseMarriageAsync(UpdateDependentMarriagePrcRequest command);





    /// <summary>
    /// ثبت طلاق  افراد تحت تکفل - فرزند
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<ProcedureResultDto> UpdateDependentChildDivorceAsync(SetDependentDivorceModel command);


    /// <summary>
    /// ثبت طلاق افراد تحت تکفل - همسر
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<ProcedureResultDto> UpdateDependentSpouseDivorceAsync(SetDependentDivorceModel command);

    /// <summary>
    /// غیرفعال کردن تکفل
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<ProcedureResultDto> DeActiveCaseDependent(DeActiveDependentV4Model command);

    /// <summary>
    /// دریافت کمیسیون از کارافتادگی فرد تحت تکفل
    /// </summary>
    /// <param name="codm"></param>
    /// <param name="dependentId"></param>
    /// <returns></returns>
    Task<GetDependentPensionCommissionModel> GetDependentPensionCommission(int codm, long dependentId);
}


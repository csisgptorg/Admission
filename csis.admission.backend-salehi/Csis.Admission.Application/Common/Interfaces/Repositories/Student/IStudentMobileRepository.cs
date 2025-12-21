using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Features.StudentMobiles.Dtos;

namespace Csis.Admission.Application.Common.Interfaces.Repositories;

/// <inheritdoc/>
public interface IStudentMobileRepository
{
    /// <inheritdoc/>
    Task<FamilyMobileDto[]> GetFamily(int codm);

    /// <inheritdoc/>
    Task<ProcedureResultDto> Update(UpdateStudentPhoneRepoCommand command);

    ///// <inheritdoc/>
    Task<ProcedureResultDto> Update(UpdateStudentMobileRepoCommand command);

    /// <inheritdoc/>
    Task<ProcedureResultDto> Update(UpdateStudentTelephoneRepoCommand command);

    /// <inheritdoc/>
    Task<ProcedureResultDto> UpdateDependent(UpdateDependentMobileRepoCommand command);
}


using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Students.Dtos;

/// <summary>
/// اطلاعات شناسنامه همسر طلبه
/// </summary>
public record StudentSpouseDto : BaseDto<StudentSpouseDto, StudentDependent,long>
{


    /// <summary>
    /// نام
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    public string LastName { get; set; }
}

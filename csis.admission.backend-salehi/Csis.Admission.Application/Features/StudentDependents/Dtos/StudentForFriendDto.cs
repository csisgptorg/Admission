using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.StudentDependents.Dtos;
/// <inheritdoc/>
public sealed record StudentForFriendDto : BaseDto<StudentForFriendDto, Student>
{
    /// <inheritdoc/>
    public string FirstName{ get; set; }

    /// <inheritdoc/>
    public string LastName{ get; set; }
}

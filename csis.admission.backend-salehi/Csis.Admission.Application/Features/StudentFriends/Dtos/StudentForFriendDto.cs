using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.StudentFriends.Dtos;
/// <inheritdoc/>
public sealed record StudentForFriendDto : BaseDto<StudentForFriendDto, StudentSummary>
{
    /// <inheritdoc/>
    public string FirstName{ get; set; }

    /// <inheritdoc/>
    public string LastName{ get; set; }
}

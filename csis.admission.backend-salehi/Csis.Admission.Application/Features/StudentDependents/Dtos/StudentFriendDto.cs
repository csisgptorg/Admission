using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.StudentDependents.Dtos;
/// <inheritdoc/>
public sealed record StudentFriendDto : BaseDto<StudentFriendDto, StudentFriend>
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public int? FriendCodm { get; set; }

    /// <inheritdoc/>
    public long? Mobile { get; set; }

    /// <inheritdoc/>
    public string Friend { get; set; }

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<StudentFriend, StudentFriendDto> mapping) {
        mapping.ForMember(dto => dto.Friend, config => config.MapFrom(model => !string.IsNullOrEmpty(model.FirstName) || !string.IsNullOrEmpty(model.LastName) ? model.FirstName + " " + model.LastName : model.FirstName + model.LastName));
    }
}

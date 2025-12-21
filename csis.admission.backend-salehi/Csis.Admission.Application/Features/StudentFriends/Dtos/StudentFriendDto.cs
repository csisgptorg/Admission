using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.StudentFriends.Dtos;
/// <inheritdoc/>
public sealed record StudentFriendDto : BaseDto<StudentFriendDto, StudentFriend>
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public int? FriendCodm { get; set; }

    /// <inheritdoc/>
    public string? Mobile { get; set; }

    /// <inheritdoc/>
    public string Friend { get; set; }

    /// <inheritdoc/>
    public override void CustomMappings(IMappingExpression<StudentFriend, StudentFriendDto> mapping) {
        mapping.ForMember(dto => dto.Friend, config => config.MapFrom(model => model.FirstName + " " + model.LastName));
        mapping.ForMember(dto => dto.Mobile, config => config.MapFrom(model => model.Mobile.HasValue? model.Mobile.ToString().Insert(0, "0") : null));
    }
}

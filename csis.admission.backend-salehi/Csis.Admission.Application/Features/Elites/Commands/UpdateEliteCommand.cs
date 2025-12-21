using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Elites.Commands;

/// <summary>
/// »—Ê“—”«‰? ‰Œ»ê«‰
/// </summary>
public sealed record UpdateEliteCommand : BaseCommandDto<UpdateEliteCommand, Elite>, IRequest<int>
{
    /// <summary>‘‰«”Â</summary>
    public int Id { get; set; }

    /// <summary>òœ „—ò“ Œœ„« </summary>
    public int Codm { get; set; }

    /// <summary>‰Ê⁄ ‰Œ»ê?</summary>
    public short? EliteTypeId { get; set; }

 /// <summary>”ÿÕ ‰Œ»ê?</summary>
    public short? EliteLevelId { get; set; }

    /// <summary> «—?Œ ‘—Ê⁄</summary>
    public string? StartDate { get; set; }

    /// <summary> «—?Œ Å«?«‰</summary>
    public string? EndDate { get; set; }

    /// <summary>„—Ã⁄  «??œ</summary>
    public string ApprovalCenterTitle { get; set; }

    /// <summary>‘‰«”Â œ—ŒÊ«” </summary>
    public long? RequestId { get; set; }

    public override void ReverseCustomMappings(IMappingExpression<UpdateEliteCommand, Elite> mapping) {
        base.ReverseCustomMappings(mapping);
        mapping.ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.StringDateToInt()));
        mapping.ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.StringDateToInt()));
    }
}

internal sealed class UpdateEliteCommandHandler(IRepository<Elite> repo)
    : IRequestHandler<UpdateEliteCommand, int>
{
    public async Task<int> Handle(UpdateEliteCommand command, CancellationToken cancellationToken) {
        var elite = await repo.GetByIdAsTrackingAsync(command.Id, cancellationToken: cancellationToken);
        
        if ( elite == null ) {
            throw new RecordNotFoundException<Elite>(command.Id);
        }

        var updatedElite = command.ToEntity(elite);
   await repo.UpdateAsync(updatedElite, cancellationToken: cancellationToken);
 
  return updatedElite.Id;
    }
}

using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.BlockServices.Commands;

/// <summary>ویرایش</summary>
public sealed record UpdateStudentBlockServiceCommand : BaseCommandDto<UpdateStudentBlockServiceCommand, StudentBlockService>, IRequest
{
    /// <summary>شناسه</summary>
    public int Id { get; init; }

    /// <summary>علت</summary>
    public string Reason { get; init; }
}

internal sealed class UpdateStudentBlockServiceCommandHandler(IRepository<StudentBlockService> repo)
    : IRequestHandler<UpdateStudentBlockServiceCommand>
{
    public async Task Handle(UpdateStudentBlockServiceCommand command, CancellationToken cancellation) {

        var studentBlockService= await repo.GetByIdAsTrackingAsync(command.Id, false, cancellation)
                    ?? throw new CommandValidationException("رکورد یافت نشد.");

        studentBlockService = command.ToEntity(studentBlockService);
        await repo.UpdateAsync(studentBlockService, true, cancellation);
    }
}

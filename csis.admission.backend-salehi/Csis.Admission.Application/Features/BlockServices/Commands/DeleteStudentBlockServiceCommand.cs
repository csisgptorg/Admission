namespace Csis.Admission.Application.Features.BlockServices.Commands;

/// <summary>حذف</summary>
public sealed record DeleteStudentBlockServiceCommand(int Id) : IRequest;

internal sealed class DeleteStudentBlockServiceCommandHandler(IRepository<StudentBlockService, int> repo) 
    : IRequestHandler<DeleteStudentBlockServiceCommand>
{
    public async Task Handle(DeleteStudentBlockServiceCommand command, CancellationToken cancellation) {
        if ( !await repo.DeleteAsync(command.Id, true,cancellation) ) {
            throw new CommandValidationException("رکورد یافت نشد.");
        }
    }
}

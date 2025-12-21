namespace Csis.Admission.Application.Features.Teaches.Commands;

/// <summary>
/// حذف سابقه تدریس برای طلبه
/// </summary>
/// <param name="Id"></param>
public sealed record DeleteTeachCommand(int Codm, int Id) : IRequest<int>;

internal sealed class DeleteTeachCommandHandler(IRepository<Teach> teachRepo) : IRequestHandler<DeleteTeachCommand, int>
{
    public async Task<int> Handle(DeleteTeachCommand request, CancellationToken cancellationToken) {
        if ( !await teachRepo.DeleteAsync(request.Id, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException($"حذف سابقه تدریس برای طلبه با شناسه {request.Id} یافت نشد");
        }

        return request.Id;
    }
}


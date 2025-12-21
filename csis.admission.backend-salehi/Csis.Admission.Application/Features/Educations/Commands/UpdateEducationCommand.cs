using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Educations.Commands;

/// <inheritdoc/>
public sealed record UpdateEducationCommand : BaseCommandDto<UpdateEducationCommand, Education>, IRequest
{
    /// <summary>کد مرکز</summary>
    public int Codm { get; init; }
    /// <summary> مرجع حوزوی</summary>
    public ApprovalCenter? ApprovalCenter { get; init; }
    /// <summary>شماره پرونده در مرجع حوزوی</summary>
    public long? CaseNumInApprovalCenter { get; init; }
}

internal sealed class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand>
{
    private readonly IRepository<Education> _repo;
    public UpdateEducationCommandHandler(IRepository<Education> repo) {
        _repo = repo;
    }

    public async Task Handle(UpdateEducationCommand request, CancellationToken cancellationToken) {
        var education = await _repo.GetOneAsTrackingAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken)
            ?? throw new CommandValidationException($"تحصیلات حوزوی برای کد مرکز {request.Codm} یافت نشد.");

        var entity = request.ToEntity(education);
        await _repo.UpdateAsync(entity);
    }
}

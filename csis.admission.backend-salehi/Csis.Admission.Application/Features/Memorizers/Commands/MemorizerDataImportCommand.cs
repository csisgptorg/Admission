using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Memorizers.Commands;

/// <summary>
/// ارتباط داده ای - حافظین
/// </summary>
public sealed record class MemorizerDataImportCommand : IRequest<int>
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// تعداد جزء حفظ شده
    /// </summary>
    public int JozCount { get; init; }

    /// <summary>
    /// مرجع تایید کنننده حوزوی
    /// </summary>
    public ApprovalCenter ApprovalCenter { get; init; }

    /// <summary>
    /// تاریخ انقضاء
    /// </summary>
    public int ExpireDate { get; init; }
}

internal sealed class MemorizerDataImportCommandHandler : IRequestHandler<MemorizerDataImportCommand, int>
{
    private readonly IRepository<Memorizer> _memorizerRepo;
    private readonly IStudentRepository _studentRepository;

    public MemorizerDataImportCommandHandler(IRepository<Memorizer> memorizerRepo, IStudentRepository studentRepository) {
        _memorizerRepo = memorizerRepo;
        _studentRepository = studentRepository;
    }

    public async Task<int> Handle(MemorizerDataImportCommand request, CancellationToken cancellationToken) {
        _ = await _studentRepository.GetByCodm(request.Codm)
           ?? throw new CommandValidationException("کد مرکز خدمات نامعتبر می باشد.");

        var memorizer = await _memorizerRepo.GetOneAsTrackingAsync(predicate:
            x => x.Kind == MemorizationType.QuranMemorizers &&
            x.Codm == request.Codm &&
            x.ApprovalCenter == request.ApprovalCenter,
            cancellationToken: cancellationToken);

        if ( memorizer is not null ) {
            memorizer.JozCount = request.JozCount;
            memorizer.ExpireDate = request.ExpireDate;
            await _memorizerRepo.UpdateAsync(memorizer, cancellationToken: cancellationToken);
            return memorizer.Id;
        }

        memorizer = new Memorizer {
            Codm = request.Codm,
            Kind = MemorizationType.QuranMemorizers,
            JozCount = request.JozCount,
            ApprovalCenter = request.ApprovalCenter,
            ExpireDate = request.ExpireDate
        };

        await _memorizerRepo.InsertAsync(memorizer, cancellationToken: cancellationToken);

        return memorizer.Id;
    }
}

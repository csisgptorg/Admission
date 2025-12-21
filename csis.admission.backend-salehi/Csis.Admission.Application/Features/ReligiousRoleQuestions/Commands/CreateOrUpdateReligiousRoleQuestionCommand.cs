using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.ReligiousRoleQuestions.Commands;

/// <summary>
/// پرسشنامه نقش آفرینی
/// </summary>
public sealed record class CreateOrUpdateReligiousRoleQuestionCommand : BaseCommandDto<CreateOrUpdateReligiousRoleQuestionCommand, ReligiousRoleQuestion>, IRequest
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <summary>
    /// آیا ملبس است ؟
    /// </summary>
    public bool IsReligiouslyDressed { get; init; }

    /// <summary>
    /// توضیحات ملبس - اجباری
    /// </summary>
    public string ReligiouslyDressedDescription { get; init; }

    /// <summary>
    /// فعالیت در زمینه های حوزوی
    /// </summary>
    public bool HasRole { get; init; }

    /// <summary>
    /// توضیحات فعالیت در زمینه های حوزوی
    /// </summary>
    public string HasRoleDescription { get; init; }

    /// <summary>
    /// نوع نقش آفرینی
    /// </summary>
    public ReligiousRoleType? ReligiousRoleType { get; init; }

    /// <summary>
    /// دلیل عدم فعالیت
    /// </summary>
    public NotHavingRoleCause? NotHavingRoleCause { get; init; }

    /// <summary>
    /// کد های مرکز معرفین
    /// </summary>
    public List<int> StudentReagentCodms { get; set; } = [];

    /// <inheritdoc/>
    public long? RequestId { get; init; }
}

internal sealed class CreateOrUpdateReligiousRoleQuestionCommandHandler : IRequestHandler<CreateOrUpdateReligiousRoleQuestionCommand>
{
    private readonly IRepository<ReligiousRoleQuestion> _religiousRoleRepo;
    private readonly IRepository<UploadedFile> _uploadRepo;
    private readonly IRepository<RequestDocument, long> _documentRepo;

    public CreateOrUpdateReligiousRoleQuestionCommandHandler(IRepository<ReligiousRoleQuestion> religiousRoleRepo, IRepository<UploadedFile> uploadRepo, IRepository<RequestDocument, long> documentRepo) {
        _religiousRoleRepo = religiousRoleRepo;
        _uploadRepo = uploadRepo;
        _documentRepo = documentRepo;
    }

    public async Task Handle(CreateOrUpdateReligiousRoleQuestionCommand request, CancellationToken cancellationToken) {
        var religiousRule = await _religiousRoleRepo.GetOneAsTrackingAsync(x => x.Codm == request.Codm);

        if ( religiousRule is null)  {
            await _religiousRoleRepo.InsertAsync(request.ToEntity(), cancellationToken: cancellationToken);
        } else {

            await _religiousRoleRepo.UpdateAsync(request.ToEntity(religiousRule), cancellationToken: cancellationToken);
        }
    }
}

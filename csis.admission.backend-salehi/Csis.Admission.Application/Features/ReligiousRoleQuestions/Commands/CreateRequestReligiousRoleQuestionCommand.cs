using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.ReligiousRoleQuestions.Commands;

/// <summary>
/// ایجاد درخواست پرسشنامه نقش آفرینی
/// </summary>
public sealed record class CreateRequestReligiousRoleQuestionCommand : IRequest
{
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
    public int[] StudentReagentCodms { get; init; } = [];

    /// <summary>مدرک نقش آفرینی</summary>
    public Guid? FileId { get; set; }
}

internal sealed class CreateRequestReligiousRoleQuestionCommandHandler : IRequestHandler<CreateRequestReligiousRoleQuestionCommand>
{
    private readonly IRequestService _requestService;
    private readonly IRepository<ReligiousRoleQuestion> _religiousRoleRepo;
    private readonly IRepository<UploadedFile> _uploadRepo;
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    private readonly IStudentRepository _studentRepository;

    public CreateRequestReligiousRoleQuestionCommandHandler(
        IRepository<ReligiousRoleQuestion> religiousRoleRepo,
        IRepository<UploadedFile> uploadRepo,
        ICsisAuthenticatedUserService authenticatedUserService,
        IRequestService requestService,
        IStudentRepository studentRepository) {
        _religiousRoleRepo = religiousRoleRepo;
        _uploadRepo = uploadRepo;
        _authenticatedUserService = authenticatedUserService;
        _requestService = requestService;
        _studentRepository = studentRepository;
    }

    public async Task Handle(CreateRequestReligiousRoleQuestionCommand command, CancellationToken cancellationToken) {
        var codm = await _authenticatedUserService.GetStudentCodmAsync();

        await ValidateAsync(command);

        var religiousRoleCount = await _religiousRoleRepo.CountAsync(x => x.Codm == codm.ToInt(), cancellationToken: cancellationToken);

        var flow= command.HasRole ? RequestFlow.DualStudents : RequestFlow.DirectRegistration;
        var requestCommand = new CreateRequestCommand(command, flow);
        if ( command.FileId.HasValue ) {
            requestCommand.AddDocument(command.FileId.Value);
        }
        await _requestService.Create(requestCommand, cancellationToken);
    }

    private async Task ValidateAsync(CreateRequestReligiousRoleQuestionCommand request) {

        //TODO : چندبار ValidateReligiousRoleQuestionByCodm صدا زده میشه
        var errors = new List<string>();
        foreach ( var codm in request.StudentReagentCodms ) {

           var result = await _studentRepository.ValidateReligiousRoleQuestionByCodm(codm);

            if (!result.IsValid ) {
                errors.Add($"{result.Codm} : {result.Message}" );
            }
        }

        if ( errors.Count > 0 ) {
            throw new CommandValidationException(string.Join(Environment.NewLine, errors));
        }
    }
}

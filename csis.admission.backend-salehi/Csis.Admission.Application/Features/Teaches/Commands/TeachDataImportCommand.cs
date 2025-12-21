using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Teaches.Commands;

/// <summary>
/// ارتباط داده ای  - تدریس
/// </summary>
public sealed record class TeachDataImportCommand : IRequest<int>
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// استان
    /// </summary>
    public short ProvinceId { get; init; }

    /// <summary>
    /// شهرستان
    /// </summary>
    public short CityId { get; init; }

    /// <summary>
    /// مدرسه
    /// </summary>
    public short SchoolId { get; init; }

    /// <summary>
    /// سال تحصیلی
    /// </summary>
    public short EducationYearId { get; init; }

    /// <summary>
    /// نیمسال تحصیلی
    /// </summary>
    public EducationSemester? EducationSemester { get; init; }

    /// <summary>
    /// مقطع تحصیلی که در آن تدریس میشود
    /// </summary>
    public TeachEducationLevel EducationLevel { get; init; }

    /// <summary>
    /// Lesson
    /// </summary>
    public string Lesson { get; init; }

    /// <summary>
    /// مرکز حوزوی
    /// </summary>
    public ApprovalCenter ApprovalCenter { get; init; }

    /// <summary>
    /// شناسه تبلیغ در مرکز حوزوی
    /// </summary>
    public string RecordIdInApprovalCenter { get; init; }
}

internal sealed class TeachDataImportCommandHandler : IRequestHandler<TeachDataImportCommand, int>
{
    private readonly IRepository<Teach> _teachRepo;
    private readonly IMediator _mediator;
    private readonly IStudentRepository _studentRepository;

    public TeachDataImportCommandHandler(
        IRepository<Teach> teachRepo,
        IMediator mediator,
        IStudentRepository studentRepository) {
        _teachRepo = teachRepo;
        _mediator = mediator;
        _studentRepository = studentRepository;
    }

    public async Task<int> Handle(TeachDataImportCommand request, CancellationToken cancellationToken) {

        _ = await _studentRepository.GetByCodm(request.Codm)
            ?? throw new CommandValidationException("کد مرکز خدمات نامعتبر می باشد.");

        await ValidateRecordIdApprovalCenterAsync(request.ApprovalCenter, request.RecordIdInApprovalCenter);

        await ValidateEducationAndCodmAsync(request);

        return await _mediator.Send(new CreateTeachCommand() {
            Codm = request.Codm,
            ProvinceId = request.ProvinceId,
            CityId = request.CityId,
            SchoolId = request.SchoolId,
            EducationYearId = request.EducationYearId,
            EducationSemester = request.EducationSemester,
            EducationLevel = request.EducationLevel,
            Lesson = request.Lesson,
            ApprovalCenter = request.ApprovalCenter,
            RecordIdInApprovalCenter = request.RecordIdInApprovalCenter,
        }, cancellationToken);

    }

    /// <summary>
    /// بررسی تکراری نبودن شناسه تبلیغ در مرکز حوزوی
    /// </summary>
    /// <param name="approvalCenter"></param>
    /// <param name="recordIdInApprovalCenter"></param>
    /// <returns></returns>
    /// <exception cref="CommandValidationException"></exception>
    private async Task ValidateRecordIdApprovalCenterAsync(ApprovalCenter approvalCenter, string recordIdInApprovalCenter) {
        if ( await _teachRepo.ExistsAsync(x =>
            x.ApprovalCenter == approvalCenter &&
            x.RecordIdInApprovalCenter == recordIdInApprovalCenter) ) {
            throw new CommandValidationException("شناسه تبلیغ در مرکز حوزوی تکراری است.");
        }
    }

    /// <summary>
    /// بررسی تکراری نبودن کد مرکز  و استان و شهرستان و مدرسه و سال تحصیلی و نیمسال تحصیلی و مقطع تدریس و نام درس
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="CommandValidationException"></exception>
    private async Task ValidateEducationAndCodmAsync(TeachDataImportCommand request) {
        if ( await _teachRepo.ExistsAsync(x =>
            x.Codm == request.Codm &&
            x.ProvinceId == request.ProvinceId &&
            x.CityId == request.CityId &&
            x.SchoolId == request.SchoolId &&
            x.EducationYearId == request.EducationYearId &&
            x.EducationSemester == request.EducationSemester &&
            x.EducationLevel == request.EducationLevel &&
            x.Lesson == request.Lesson) ) {
            throw new CommandValidationException("خطا در تکراری بودن داده های دریافتی از کاربر");
        }
    }
}

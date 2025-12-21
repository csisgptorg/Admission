using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.UniversityEducations.Commands;

/// <summary>
/// ارتباطات داده ای - تحصیلات دانشگاهی
/// </summary>
public sealed record class UniversityEducationDataImportCommand : BaseCommandDto<UniversityEducationDataImportCommand, UniversityEducation>,  IRequest<int>
{
    /// <summary>
    /// کد مرکز خدمات
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// شناسه تکفل
    /// </summary>
    public long? DependentId { get; init; }

    /// <summary>
    /// در حال تحصیل
    /// </summary>
    public bool InStudy { get; init; }

    /// <summary>
    /// مدرک تحصیلی
    /// </summary>
    public StudyLevel? StudyLevel { get; init; }

    /// <summary>
    /// رشته
    /// </summary>
    public string CourseStudy { get; init; }

    /// <summary>
    /// نوع دانشگاه
    /// </summary>
    public UniversityTypeEnum? UniversityType { get; init; }

    /// <summary>
    /// نام دانشگاه
    /// </summary>
    public string UniversityName { get; init; }

    /// <summary>
    /// نام استان
    /// </summary>
    public string ProvinceTitle { get; init; }

    /// <summary>
    /// تاریخ شروع
    /// </summary>
    public int? StartDate { get; init; }

    /// <summary>
    /// تاریخ پایان
    /// </summary>
    public int? EndDate { get; init; }

    /// <summary>
    /// معدل
    /// </summary>
    public double? Average { get; init; }

    /// <summary>
    /// تاریخ اعتبار
    /// </summary>
    public int? ValidityDate { get; init; }
}

internal sealed class UniversityEducationDataImportCommandHandler : IRequestHandler<UniversityEducationDataImportCommand, int>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IRepository<DependentSummary, long> _studentDependentRepository;
    private readonly IRepository<UniversityEducation> _universityRepo;

    public UniversityEducationDataImportCommandHandler(
        IStudentRepository studentRepository, IRepository<DependentSummary, long> studentDependentRepository, IRepository<UniversityEducation> universityRepo) {
        _studentRepository = studentRepository;
        _studentDependentRepository = studentDependentRepository;
        _universityRepo = universityRepo;
    }

    public async Task<int> Handle(UniversityEducationDataImportCommand request, CancellationToken cancellationToken) {
        _ = await _studentRepository.GetStudentInfoByCodm(request.Codm)
            ?? throw new CommandValidationException("کد مرکز خدمات معتبر نمی باشد");

        if ( !await _studentDependentRepository.ExistsAsync(x =>
            x.Codm == request.Codm && x.Id == request.DependentId, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException("شناسه تکفل نامعتبر است.");
        }

        var universityEducation = request.ToEntity();
        await _universityRepo.InsertAsync(universityEducation, cancellationToken: cancellationToken);

        return universityEducation.Id;
    }
}

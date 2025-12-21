using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Features.UniversityEducations.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.UniversityEducations.Commands;

/// <summary>ثبت تحصیلات دانشگاهی</summary>
public sealed record class CreateUniversityEducationCommand(int Codm, long? DependentId, bool Confirmed) : IRequest<List<StudentUniversityEducationDto>>;

internal sealed class CreateUniversityEducationCommandHandler : IRequestHandler<CreateUniversityEducationCommand, List<StudentUniversityEducationDto>>
{
    private readonly ICsisWsmService _csisWsmService;
    private readonly IStudentRepository _studentRepository;
    private readonly IRepository<DependentSummary, long> _studentDependentRepository;
    private readonly IRepository<UniversityEducation> _universityRepo;
    private readonly IMapper _mapper;

    public CreateUniversityEducationCommandHandler(
        ICsisWsmService csisWsmService,
        IStudentRepository studentRepository,
        IRepository<UniversityEducation> universityRepo,
        IMapper mapper,
        IRepository<DependentSummary, long> studentDependentRepository) {
        _csisWsmService = csisWsmService;
        _studentRepository = studentRepository;
        _universityRepo = universityRepo;
        _mapper = mapper;
        _studentDependentRepository = studentDependentRepository;
    }

    public async Task<List<StudentUniversityEducationDto>> Handle(CreateUniversityEducationCommand request, CancellationToken cancellationToken) {

        var student = await _studentRepository.GetStudentInfoByCodm(request.Codm);

        if ( request.DependentId is not null && !await _studentDependentRepository.ExistsAsync(x =>
            x.Codm == request.Codm && x.Id == request.DependentId, cancellationToken: cancellationToken) ) {
            throw new CommandValidationException("شناسه تکفل نامعتبر است.");
        }

        var model = new InquiryCertificateModel(request.Codm, request.DependentId,student.NationalCode, "");
        var result = await _csisWsmService.GetInquiryCertificate(model, cancellationToken);

        List<UniversityEducation> universityEducations = [];

        //foreach ( var item in result ) {

        //    if ( request.Confirmed && !Validate(item) ) {
        //        continue;
        //    }

        //    if ( request.Confirmed && await CheckDuplicateRecordAsync(item) ) {
        //        continue;
        //    }

        //    var universityEducation = new UniversityEducation {
        //        Codm = request.Codm,
        //        DependentId = request.DependentId,
        //        InStudy = InStudyMapping(item),
        //        StudyLevel = StudyLevelMapping(item),
        //        CourseStudy = item.StudentMsrtInfo.CourseStudyDesc,
        //        UniversityType = UniversityTypeMapping(item),
        //        UniversityName = item.StudentInfo.UniversityDesc,
        //        ProvinceTitle = item.StudentMsrtInfo.ProvinceDesc ?? "",
        //        StartDate = item.StudentMsrtInfo.StartDate.StringDateToInt(),
        //        EndDate = item.StudentMsrtInfo.StopDate?.StringDateToInt(),
        //        Average = item.StudentInfo.TotalAverage,
        //        //Todo : ValidityDate = item.StudentMsrtInfo.ValidityDate?.StringDateToInt(),

        //    };

        //    universityEducations.Add(universityEducation);
        //}

        if ( request.Confirmed ) {
            await _universityRepo.InsertAsync(universityEducations, cancellationToken: cancellationToken);
            return default;
        } else {
            return _mapper.Map<List<StudentUniversityEducationDto>>(universityEducations);
        }
    }

    /// <summary>
    /// اعتبارسنجی داده های دریافتی از سرویس
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private static bool Validate(ResponseInquiryCertificateData data) {
        var inStudy = data.StudentMsrtInfo.StudentStatusId;
        if ( inStudy is not 810001 and not 800000 ) {
            return false;
        }

        // TODO خودم کامنت کردم باید بررسی شود
        // نام دانشگاه
        //if ( string.IsNullOrEmpty(data.StudentInfo.UniversityDesc) ) {
        //    return false;
        //}

        // رشته تحصیلی
        if ( string.IsNullOrEmpty(data.StudentMsrtInfo.CourseStudyDesc) ) {
            return false;
        }

        if ( inStudy == 810001 && string.IsNullOrEmpty(data.StudentMsrtInfo.StopDate) ) {
            return false;
        }

        var studyLevel = data.StudentMsrtInfo.StudyLevelId;
        if ( studyLevel is not 210002 and not 210003 and not 210004 and not 210005 and not 210006
            and not 210008 and not 210012 ) {
            return false;
        }

        var universityType = data.StudentMsrtInfo.StudyingModeId;
        if ( universityType is not 440001 and not 440002 and not 440003 and not 440005 and not 440006
            and not 440007 and not 440008 and not 440009 ) {
            return false;
        }

        if ( string.IsNullOrEmpty(data.StudentMsrtInfo.StartDate) ) {
            return false;
        }

        return true;
    }

    /// <summary>
    /// چک کردن رکورد تکراری
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    //private async Task<bool> CheckDuplicateRecordAsync(ResponseInquryCertificateData data) {
    //    if ( await _universityRepo.ExistsAsync(x =>
    //        x.StudyLevel == (StudyLevel) data.StudentMsrtInfo.StudyLevelId &&
    //        x.UniversityType == (UniversityTypeEnum) data.StudentMsrtInfo.StudyingModeId &&
    //        x.CourseStudy == data.StudentMsrtInfo.CourseStudyDesc &&
    //        //x.UniversityName == data.StudentInfo.UniversityDesc &&
    //        x.StartDate == data.StudentMsrtInfo.StartDate.StringDateToInt()
    //    ) ) {
    //        return false;
    //    }

    //    return true;
    //}

}

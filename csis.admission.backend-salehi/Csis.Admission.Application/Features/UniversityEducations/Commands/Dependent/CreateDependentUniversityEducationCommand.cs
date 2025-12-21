using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.UniversityEducations.Commands;

/// <summary>ثبت تحصیلات دانشگاهی تکفل</summary>
public sealed record class CreateDependentUniversityEducationCommand :
    BaseCommandDto<CreateDependentUniversityEducationCommand, UniversityEducation>, IRequest<int>
{
    /// <summary>شناسه تکفل</summary>
    public long DependentId { get; init; }

    /// <summary>در حال تحصیل</summary>
    public bool InStudy { get; init; }

    /// <summary>مدرک تحصیلی</summary>
    public StudyLevel? StudyLevel { get; init; }

    /// <summary>رشته</summary>
    public string CourseStudy { get; init; }

    /// <summary>نوع دانشگاه</summary>
    public UniversityTypeEnum? UniversityType { get; init; }

    /// <summary>نام دانشگاه</summary>
    public string UniversityName { get; init; }

    /// <summary>نام استان</summary>
    public string ProvinceTitle { get; init; }

    /// <summary>تاریخ شروع</summary>
    public string StartDate { get; init; }

    /// <summary>تاریخ پایان</summary>
    public string EndDate { get; init; }

    /// <summary>معدل</summary>
    public double? Average { get; init; }

    /// <summary>تاریخ اعتبار</summary>
    public string ValidityDate { get; init; }

    /// <summary>شناسه فایل</summary>
    public Guid FileId { get; init; }

    /// <summary>شناسه درخواست</summary>
    public long RequestId { get; set; }

    /// <inheritdoc/>
    public override void ReverseCustomMappings(IMappingExpression<CreateDependentUniversityEducationCommand, UniversityEducation> mapping) {
        mapping.ForMember(model => model.StartDate, config => config.MapFrom(dto => dto.StartDate.StringDateToInt()));
        mapping.ForMember(model => model.EndDate, config => config.MapFrom(dto => dto.EndDate.StringDateToInt()));
        mapping.ForMember(model => model.ValidityDate, config => config.MapFrom(dto => dto.ValidityDate.StringDateToInt()));
    }
}

internal sealed class CreateDependentUniversityEducationCommandHandler(
    IRepository<UniversityEducation> universityRepo, 
    IRepository<DependentSummary,long> dependentRepo)
    : IRequestHandler<CreateDependentUniversityEducationCommand, int>
{
    public async Task<int> Handle(CreateDependentUniversityEducationCommand command, CancellationToken cancellationToken) {
        var universityEducation = command.ToEntity();

        var dependent = await dependentRepo.GetByIdAsync(command.DependentId,false,cancellationToken: cancellationToken);
        universityEducation.Codm=dependent.Codm;

        await universityRepo.InsertAsync(universityEducation, cancellationToken: cancellationToken);
        return universityEducation.Id;
    }
}

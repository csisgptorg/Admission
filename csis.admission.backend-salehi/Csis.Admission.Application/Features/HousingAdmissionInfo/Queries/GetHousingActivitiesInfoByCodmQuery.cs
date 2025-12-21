using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.HousingAdmissionInfo.Dtos;

namespace Csis.Admission.Application.Features.HousingAdmissionInfo.Queries;

/// <summary>
/// دریافت اطلاعات فعالیت‌های علمی و فرهنگی برای سامانه مسکن
/// </summary>
/// <param name="Codm">کد مرکز</param>
public sealed record GetHousingActivitiesInfoByCodmQuery(int Codm) : IRequest<HousingActivitiesInfoDto>;

internal sealed class GetHousingActivitiesInfoByCodmQueryHandler(
    IRepository<Elite> eliteRepo,
    IRepository<EliteLevel, short> eliteLevelRepo,
    IRepository<Teach> teachRepo,
    IRepository<TeachGrade> teachGradeRepo,
    IRepository<Preach> preachRepo,
    IRepository<PreachGrade> preachGradeRepo,
    IRepository<Research> researchRepo,
    IRepository<ResearchGrade> researchGradeRepo,
    IRepository<Education> educationRepo)
    : IRequestHandler<GetHousingActivitiesInfoByCodmQuery, HousingActivitiesInfoDto>
{
    public async Task<HousingActivitiesInfoDto> Handle(GetHousingActivitiesInfoByCodmQuery request, CancellationToken cancellationToken) {
        // دریافت لیست نخبگی
        var elites = await eliteRepo.GetAllAsync(
            x => x.Codm == request.Codm,
            cancellationToken: cancellationToken);

        var eliteList = new List<EliteItemModel>();
        EliteLevelModel eliteLevel = null;

        foreach ( var elite in elites ) {
            if ( elite.EliteTypeId.HasValue ) {
                eliteList.Add(new EliteItemModel(
                    Id: elite.EliteTypeId,
                    Title: elite.EliteType?.Title ?? elite.EliteTypeId.ToString(),
                    StartDate: elite.StartDate.HasValue ? elite.StartDate.Value.IntDateToString() : null,
                    EndDate: elite.EndDate.HasValue ? elite.EndDate.Value.IntDateToString() : null
                ));
            }

            if ( elite.EliteLevelId.HasValue && eliteLevel == null ) {
                var level = await eliteLevelRepo.GetByIdAsync(elite.EliteLevelId.Value, cancellationToken: cancellationToken);
                if ( level != null ) {
                    eliteLevel = new EliteLevelModel(
                        Id: level.Id,
                        Title: level.Title
                    );
                }
            }
        }

        // دریافت لیست تدریس
        var teaches = await teachRepo.GetAllAsync(
            x => x.Codm == request.Codm, cancellationToken: cancellationToken, x => x.EducationYear, x => x.School);

        var teachingList = new List<TeachingItemModel>();
        GradeLevelModel teachingLevel = null;

        foreach ( var teach in teaches ) {
            teachingList.Add(new TeachingItemModel(
                Title: teach.Lesson ?? null,
                School: teach.School?.Title ?? null,
                EducationYear: teach.EducationYear?.Title ?? null
            ));
        }

        // دریافت سطح تدریس
        var teachGrade = await teachGradeRepo.GetOneAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        if ( teachGrade != null ) {
            teachingLevel = new GradeLevelModel(
                Grade: teachGrade.Grade,
                Id: teachGrade.Id
            );
        }

        // دریافت لیست تبلیغ
        var preaches = await preachRepo.GetAllAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken);

        var propagationList = new List<PropagationItemModel>();
        GradeLevelModel propagationLevel = null;

        foreach ( var preach in preaches ) {
            propagationList.Add(new PropagationItemModel(
                Kind: preach.Kind,
                City: preach.City ?? null,
                StartDate: preach.StartDate.HasValue ? preach.StartDate.Value.IntDateToString() : null,
                EndDate: preach.EndDate.HasValue ? preach.EndDate.Value.IntDateToString() : null
            ));
        }

        // دریافت سطح تبلیغ
        var preachGrade = await preachGradeRepo.GetOneAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        if ( preachGrade != null ) {
            propagationLevel = new GradeLevelModel(
                Grade: preachGrade.Grade,
                Id: preachGrade.Id
            );
        }

        // دریافت لیست پژوهش
        var researches = await researchRepo.GetAllAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken);

        var researchList = new List<ResearchItemModel>();
        GradeLevelModel researchLevel = null;

        foreach ( var research in researches ) {
            researchList.Add(new ResearchItemModel(
                Title: research.Title ?? null,
                Type: research?.Type ?? null,
                Year: research.Year.HasValue ? research.Year : null
            ));
        }

        // دریافت سطح پژوهش
        var researchGrade = await researchGradeRepo.GetOneAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        if ( researchGrade != null ) {
            researchLevel = new GradeLevelModel(
                Grade: researchGrade.Grade,
                Id: researchGrade.Id
            );
        }

        // دریافت تحصیلات حوزوی
        var educations = await educationRepo.GetAllAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken);

        var seminaryEducationList = new List<SeminaryEducationItemModel>();

        foreach ( var education in educations ) {
            seminaryEducationList.Add(new SeminaryEducationItemModel(
                EducationStatus: education?.EducationStatus ?? null,
                ApprovalCenter: education.ApprovalCenter.HasValue ? education.ApprovalCenter : null,
                EnteringYear: education.EnteringYear.HasValue ? education.EnteringYear : null
            ));
        }

        return new HousingActivitiesInfoDto {
            EliteList = eliteList,
            EliteLevel = eliteLevel,
            TeachingList = teachingList,
            TeachingLevel = teachingLevel,
            PropagationList = propagationList,
            PropagationLevel = propagationLevel,
            ResearchList = researchList,
            ResearchLevel = researchLevel,
            SeminaryEducationList = seminaryEducationList
        };
    }
}

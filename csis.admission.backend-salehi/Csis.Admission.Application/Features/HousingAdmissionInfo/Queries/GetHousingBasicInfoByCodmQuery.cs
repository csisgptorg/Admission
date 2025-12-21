using Csis.Admission.Application.Features.Cities.Dtos;
using Csis.Admission.Application.Features.HousingAdmissionInfo.Dtos;
using Csis.Admission.Application.Features.Provinces.Dtos;

namespace Csis.Admission.Application.Features.HousingAdmissionInfo.Queries;

/// <summary>
/// دریافت اطلاعات پایه طلبه برای سامانه مسکن
/// </summary>
/// <param name="Codm">کد مرکز</param>
public sealed record GetHousingBasicInfoByCodmQuery(int Codm) : IRequest<HousingBasicInfoDto>;

internal sealed class GetHousingBasicInfoByCodmQueryHandler(
    IRepository<StudentSummary> studentRepo,
    IRepository<Branch, short> branchRepo,
    IRepository<Agency, short> agencyRepo,
    IRepository<DependentSummary, long> dependentRepo,
    IRepository<StudentEmployment> employmentRepo,
    IRepository<ReligiousRoleQuestion> religiousRoleRepo,
    IRepository<Excellent> excellentRepo,
    IRepository<Memorizer> memorizerRepo,
    IRepository<TargetScore> targetedScoreRepo,
    IRepository<Teach> teachRepo,
    IRepository<Address> addressRepository,
    IRepository<Province, short> provinceRepo,
    IRepository<City, short> cityRepo,
    IRepository<EducationYear, short> educationYearRepo)
    : IRequestHandler<GetHousingBasicInfoByCodmQuery, HousingBasicInfoDto>
{
    public async Task<HousingBasicInfoDto> Handle(GetHousingBasicInfoByCodmQuery request, CancellationToken cancellationToken) {
        // دریافت اطلاعات اصلی طلبه
        var student = await studentRepo.GetOneAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken)
            ?? throw new CommandValidationException("طلبه‌ای با این کد مرکز یافت نشد.");

        // دریافت شعبه و نمایندگی
        Branch branch = null;
        Agency agency = null;

        if ( student.BranchId.HasValue ) {
            branch = await branchRepo.GetByIdAsync((short) student.BranchId.Value, cancellationToken: cancellationToken);
        }

        if ( student.AgencyId > 0 ) {
            agency = await agencyRepo.GetByIdAsync((short) student.AgencyId, cancellationToken: cancellationToken);
        }

        // دریافت اطلاعات اشتغال
        var employment = await employmentRepo.GetOneAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken);

        // دریافت اطلاعات تلبس
        var religiousRole = await religiousRoleRepo.GetOneAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken);

        var address = await addressRepository.GetOneAsync(x => x.Codm == request.Codm && x.ProjectCode == 1, cancellationToken: cancellationToken);

        var province = address?.ProvinceId != null
            ? await provinceRepo.GetByIdAsync<ProvinceDto>((short) address.ProvinceId, cancellationToken: cancellationToken)
            : null;

        var city = address?.CityId != null
            ? await cityRepo.GetByIdAsync<CityDto>((short) address.CityId, cancellationToken: cancellationToken)
            : null;

        // محاسبه تعداد تدریس در سال تحصیلی جاری
        var currentEducationYear = await educationYearRepo.GetAllAsync(cancellationToken: cancellationToken);
        var currentYear = currentEducationYear.OrderByDescending(x => x.Id).FirstOrDefault();

        var teachingCountCurrentYear = 0;
        if ( currentYear != null ) {
            teachingCountCurrentYear = await teachRepo.CountAsync(x => x.Codm == request.Codm && x.EducationYearId == currentYear.Id, cancellationToken: cancellationToken);
        }

        // بررسی ممتازی در سال جاری
        var isExcellentInCurrentYear = false;
        if ( currentYear != null ) {
            isExcellentInCurrentYear = await excellentRepo.ExistsAsync(x => x.Codm == request.Codm && x.EducationYearId == currentYear.Id, cancellationToken: cancellationToken);
        }

        // دریافت تعداد جزء قرآن حفظ
        var memorizer = await memorizerRepo.GetOneAsync(x => x.Codm == request.Codm && x.DependentId == null, cancellationToken: cancellationToken);

        // دریافت تعداد افراد تحت تکفل
        var dependents = await dependentRepo.GetAllAsync(x => x.Codm == request.Codm && x.IsActive, cancellationToken: cancellationToken);

        var spouseCount = dependents.Count(d => d.Relation == DependentRelation.Spouse);
        var childrenCount = dependents.Count(d => d.Relation == DependentRelation.Child);

        // محاسبه امتیاز هدفمندی کل
        var targetingScores = (await targetedScoreRepo.GetOneAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken)).TotalScore;

        // بررسی سرپرست خانوار (فقط برای خانم‌ها)
        bool? isHeadOfHousehold = null;

        if ( student.Gender == Gender.Female ) {
            // اگر DependentId نداشت، سرپرست است
            var hasDependentId = dependents.Any(d => d.Id > 0);
            var isHead = !hasDependentId;

            isHeadOfHousehold = isHead;
        }

        return new HousingBasicInfoDto {
            NationalCode = student.NationalCode,
            Mobile = student.Mobile,
            IsActive = student.IsActive,
            Branch = branch?.Title,
            BranchId =  branch?.Id,
            Agency = agency?.Title,
            AgencyId = agency?.Id,
            Taraz = student.Taraz,
            EmploymentStatus = employment?.IsEmployee == true,
            LifeStatus = student.IsDead ? true : false,
            Religion = student.Religion.HasValue ? student.Religion : null,
            Gender =  student.Gender,
            IssueProvince = address?.ProvinceId.GetValueOrDefault(),
            IsMolabbas = religiousRole?.IsReligiouslyDressed == true,
            Province = province?.Title,
            ProvinceId = address?.ProvinceId.GetValueOrDefault(),
            City = city?.Title,
            CityId = address?.CityId.GetValueOrDefault(),
            TeachingCountCurrentAcademicYear = teachingCountCurrentYear,
            IsExcellentInCurrentAcademicYear = isExcellentInCurrentYear,
            QuranHifzCount = memorizer?.JozCount ?? 0,
            IsHeadOfHousehold = isHeadOfHousehold.HasValue && isHeadOfHousehold.Value,
            TotalTargetingScore = targetingScores,
            NumberOfSpouses = spouseCount,
            NumberOfDependents = childrenCount,
            Nationality = (Nationality?)student.Nationality.GetValueOrDefault(),
            SingleStatus = (SingleStatus?)student.SingleStatus,
            IsMarried = student.IsMarried ,
            IsSufficientIncome = employment?.HasSufficientIncome == true
        };
    }
}

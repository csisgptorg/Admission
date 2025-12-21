namespace Csis.Admission;

/// <inheritdoc/>
public interface ICsisAdmissionService
{
    Task<string> GetStudentProfileImageAsync(int codm, CancellationToken cancellation);
    Task<StudentCaseDtoResult> GetStudentCaseAsync(int codm, CancellationToken cancellation);
    Task<StudentTotalReportResult> GetStudentTotalReportAsync(int codm, CancellationToken cancellation);
    Task<StudentElectronicIdCardResult> GetStudentElectronicIdCardAsync(int codm, CancellationToken cancellation);
    /// <summary>دریافت اطلاعات مورد نیاز برای اجرای حقوق و دستمزد</summary>
    Task<StudentDataForPayRunResult> GetDataForPayRunByCodmAsync(int codm, CancellationToken cancellation);
    /// <summary>دریافت اطلاعات مورد نیاز برای اجرای حقوق و دستمزد بر اساس لیست کد مرکز</summary>
    Task<List<StudentDataForPayRunResult>> GetDataForPayRunByCodmListAsync(List<int> codmList, CancellationToken cancellation);
    /// <summary>دریافت اطلاعات مورد نیاز برای اجرای حقوق و دستمزد بر اساس بازه کد مرکز</summary>
    Task<List<StudentDataForPayRunResult>> GetDataForPayRunByStartEndCodmAsync(int startCodm, int endCodm, CancellationToken cancellation);
    /// <summary>دریافت بیشترین کد مرکز برای اجرای حقوق و دستمزد</summary>
    Task<int> GetMaxCodmForPayRunAsync(CancellationToken cancellationToken);
    /// <summary>دریافت لیست استان‌ها</summary>
    Task<List<ProvinceResult>> GetProvincesAsync(CancellationToken cancellationToken);
    /// <summary>دریافت لیست شهرستان‌ها بر اساس شناسه استان</summary>
    Task<CityResult[]> GetCitiesByProvinceIdAsync(short? ProvinceId, CancellationToken cancellationToken);
    /// <summary>اعتبارسنجی شعب بر اساس شناسه استان</summary>
    Task<bool> ValidateBranchesByProvinceIdAsync(short ProvinceId, short BranchId, CancellationToken cancellationToken);
    /// <summary> دریافت اطلاعات فعالیت‌های تدریس و تبلیغ دانشجویان بر اساس کد مرکز</summary>
    Task<HousingActivitiesInfoResult> GetHousingActivitiesInfoAsync(int codm, CancellationToken cancellationToken);
    /// <summary> دریافت اطلاعات پایه طلبه برای سامانه مسکن بر اساس کد مرکز</summary>
    Task<HousingBasicInfoResult> GetHousingBasicInfoByCodmAsync(int codm, CancellationToken cancellationToken);
    /// <summary> دریافت اطلاعات وضعیت مسکن طلبه بر اساس کد مرکز</summary>
    Task<HousingStatusInfoResult> GetHousingStatusInfoByCodmAsync(int codm, CancellationToken cancellationToken);
    /// <summary>دریافت استان بر اساس شناسه شعبه</summary>
    Task<BranchResult> GetProvinceByBranchIdAsync(short branchId, CancellationToken cancellationToken);
    /// <summary>دریافت دهک درآمدی بر اساس کد مرکز</summary>
    Task<Dictionary<int, short?>> GetDecileByCodmListAsync(List<int> codmList, CancellationToken cancellationToken);
    /// <summary>دریافت اطلاعات دانشجو به همراه افراد تحت تکفل</summary>
    Task<StudentWithDependentsResult> GetStudentWithDependentsAsync(int codm, CancellationToken cancellationToken);
    /// <summary>دریافت لیست تمامی شعب</summary>
    Task<List<BranchResult>> GetAllBranchesAsync(CancellationToken cancellationToken);
    /// <summary>دریافت اطلاعات دانشجو به همراه اطلاعات مورد نیاز برای خدمت ازدواج</summary>
    Task<List<StudentWithDependentInfoForDowryServiceResult>> GetStudentInfoForDowryServiceAsync(List<int?> codms, CancellationToken cancellationToken);
    /// <summary>دریافت اطلاعات قراردادهای اجاره فعال مستاجر بر اساس کد مرکز</summary>
    Task<List<ActiveTenantRentContractResult>> GetActiveTenantRentContractsByCodmAsync(int codm, CancellationToken cancellationToken);
    /// <summary>دریافت کد مرکز طلاب بر اساس کد ملی</summary>
    Task<List<GetStudentCodmByNationalCodeResult>> GetStudentsCodmByNationalCodeAsync(List<string> nationalCode, CancellationToken cancellationToken);
}

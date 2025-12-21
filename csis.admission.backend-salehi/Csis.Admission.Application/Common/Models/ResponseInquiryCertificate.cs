namespace Csis.Admission.Application.Common.Models;

/// <summary>مدل برگشتی تحصیلات دانشگاهی - وزرات علوم</summary>

public class ResponseInquiryCertificateDto
{
    /// <inheritdoc/>
    public ResponseInquiryCertificateDto(ResponseInquiryCertificateData data) {
        University = data.StudentInfo.UniversityDesc;
        CourseStudy = data.StudentMsrtInfo.CourseStudyDesc;
        AdmissionType = data.StudentMsrtInfo.StudyingModeDesc;
        StartDate = FormatPersianDate(data.StudentMsrtInfo.StartDate);
        EndDate = FormatPersianDate(data.StudentMsrtInfo.StopDate);
        Average = data.StudentInfo.TotalAverage;
        EducationStatus = data.StudentMsrtInfo.StudentStatusDesc;

        InStudy = data.StudentMsrtInfo.StudentStatusId switch {
            800000 => true,
            810001 => false,
            _ => null,
            //_ => throw new CommandValidationException("وضعیت تحصیل نامشخص"),
        };

        StudyLevel = data.StudentMsrtInfo.StudyLevelId switch {
            210002 => Domain.Enums.StudyLevel.GraduateDiploma,
            210003 => Domain.Enums.StudyLevel.BachelorDegree,
            210004 => Domain.Enums.StudyLevel.BachelorDegree,
            210005 => Domain.Enums.StudyLevel.MasterDegree,
            210006 => Domain.Enums.StudyLevel.MasterDegree,
            210008 => Domain.Enums.StudyLevel.DoctoralDegree,
            210012 => Domain.Enums.StudyLevel.DoctoralDegree,
            _ => null,
            //_ => throw new CommandValidationException("مدرک تحصیلی نامشخص"),
        };

        UniversityType = data.StudentMsrtInfo.StudyingModeId switch {
            440001 => UniversityTypeEnum.Governmental,
            440002 => UniversityTypeEnum.Governmental,
            440005 => UniversityTypeEnum.Governmental,
            440003 => UniversityTypeEnum.Virtual,
            440006 => UniversityTypeEnum.AzadUniversity,
            440007 => UniversityTypeEnum.LightMessage,
            440008 => UniversityTypeEnum.AppliedScience,
            440009 => UniversityTypeEnum.NonProfit,
            _ => null,
            //_ => throw new CommandValidationException(" خطا در ردیافت داده نامعتبر در نوع دانشگاه"),
        };

        Province = data.StudentMsrtInfo.ProvinceDesc;
    }

    /// <summary>دانشگاه</summary>
    public string University { get; }

    /// <summary>رشته تحصیلی</summary>
    public string CourseStudy { get; }

    /// <summary>نوع دوره قبولی</summary>
    public string AdmissionType { get; }

    /// <summary>تاریخ شروع</summary>
    public string StartDate { get; }

    /// <summary>تاریخ پایان</summary>
    public string EndDate { get; }

    /// <summary>معدل</summary>
    public double? Average { get; }

    /// <summary>وضعیت تحصیلی</summary>
    public string EducationStatus { get; }

    /// <summary>وضعیت تحصیلی</summary>
    public bool? InStudy { get; }

    /// <summary>مقطع تحصیلی</summary>
    public StudyLevel? StudyLevel { get; set; }

    /// <summary>نوع دانشگاه</summary>
    public UniversityTypeEnum? UniversityType { get; }

    /// <summary>استان</summary>
    public string Province { get; }

    /// <summary>تاریخ اعتبار</summary>
    public string ValidityDate { get; }

    /// <summary>استاندارد سازی تاریخ</summary>
    public static string FormatPersianDate(string date) {
        if ( string.IsNullOrWhiteSpace(date) ) {
            return null;
        }

        if ( date.Length == 4 ) {
            return $"{date}-01-01";
        }

        if ( date.Length == 9 && date.Substring(4, 1)=="-" ) {
            return $"{date.Substring(5, 4)}-01-01";
        }

        return $"{date.Substring(0, 4)}-{date.Substring(4, 2)}-{date.Substring(6, 2)}";
    }
}

/// <summary>مدل برگشتی اطلاعات تحصیلی وزارت علوم</summary>
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public class ResponseInquiryCertificateData
{
    public PersonInfoDto PersonInfo { get; set; }
    public StudentUniInfoDto StudentUniInfo { get; set; }
    public StudentMsrtInfoDto StudentMsrtInfo { get; set; }
    public StudentInfoDto StudentInfo { get; set; }
    public DateTimeOffset UpdateDate { get; set; }

    public class PersonInfoDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentificationNo { get; set; }
        public string IdentificationSerial { get; set; }
        public string IdentificatiionIssuePlace { get; set; }
        public string FatherName { get; set; }
        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string NationalCode { get; set; }
        public string CitizenCode { get; set; }
    }

    public class StudentUniInfoDto
    {
        public int? StudyLevelId { get; set; }
        public string StudyLevelDesc { get; set; }
        public int? StudyingModeId { get; set; }
        public string StudyingModeDesc { get; set; }
        public int? StudentStatusId { get; set; }
        public string StudentStatusDesc { get; set; }
        public int? CourseStudyId { get; set; }
        public string CourseStudyDesc { get; set; }
        public string StartDateYear { get; set; }
        public string StartDate { get; set; }
        public string StopDate { get; set; }
        public int? EntranceTermId { get; set; }
        public string EnteranceTermDesc { get; set; }
        public int FacultyId { get; set; }
        public string FacultyDesc { get; set; }
        public int? GenderId { get; set; }
        public string GenderDesc { get; set; }
        public int? MilitaryStatusId { get; set; }
        public string MilitaryStatusDesc { get; set; }
        public int? MarriageStatusId { get; set; }
        public string MarriageStatusDesc { get; set; }
        public int CountryId { get; set; }
        public string CountryDesc { get; set; }
        public int? ProvinceId { get; set; }
        public string ProvinceDesc { get; set; }
    }

    public class StudentMsrtInfoDto
    {
        public int? StudyLevelId { get; set; }
        public string StudyLevelDesc { get; set; }
        public int? StudyingModeId { get; set; }
        public string StudyingModeDesc { get; set; }
        public int StudentStatusId { get; set; }
        public string StudentStatusDesc { get; set; }
        public int? CourseStudyId { get; set; }
        public string CourseStudyDesc { get; set; }
        public string StartDateYear { get; set; }
        public string StartDate { get; set; }
        public string StopDate { get; set; }
        public int? EntranceTermId { get; set; }
        public string EnteranceTermDesc { get; set; }
        public int? FacultyId { get; set; }
        public string FacultyDesc { get; set; }
        public int? GenderId { get; set; }
        public string GenderDesc { get; set; }
        public int? MilitaryStatusId { get; set; }
        public string MilitaryStatusDesc { get; set; }
        public int? MarriageStatusId { get; set; }
        public string MarriageStatusDesc { get; set; }
        public int? CountryId { get; set; }
        public string CountryDesc { get; set; }
        public int? ProvinceId { get; set; }
        public string ProvinceDesc { get; set; }
    }

    public class StudentInfoDto
    {
        public string PersonCode { get; set; }
        public int? UniversityId { get; set; }
        public string UniversityDesc { get; set; }
        public double? TotalAverage { get; set; }
        public string EvaluationDate { get; set; }
        public int? InquirySource { get; set; }
        public string CertificateCode { get; set; }
    }
}

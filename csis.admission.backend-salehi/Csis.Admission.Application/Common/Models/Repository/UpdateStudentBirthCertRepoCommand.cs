namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary>بروز رسانی اطلاعات شناسنامه ای طلبه</summary>
public record UpdateStudentBirthCertRepoCommand(int Codm, string NationalCode, string FirstName, string LastName, string FatherName,
    bool IsSadat, int BirthDate, string BirthCertNumber, string BirthCertSeri, int BirthCertSerial, Gender Gender, bool IsDead);

/// <summary>بروز رسانی اطلاعات شناسنامه ای تکفل</summary>
public record UpdateDependentBirthCertRepoCommand(long Id, string NationalCode, string FirstName, string LastName,
    string FatherName, bool IsSadat, int BirthDate, string BirthCertNumber, string BirthCertSeri, int BirthCertSerial, Gender Gender,bool IsDead);

/// <summary>بروز رسانی اطلاعات شناسنامه ای طلبه غیر ایرانی</summary>
public record UpdateNonIranianStudentBirthCertRepoCommand(int Codm, string YektaCode, string FirstName, string LastName, string FatherName,
    int BirthDate, Gender Gender, short Nationality, bool IsDead);

/// <summary>غیر ایرانی بروز رسانی اطلاعات شناسنامه ای تکفل</summary>
public record UpdateNonIranianDependentBirthCertRepoCommand(long Id, string YektaCode, string FirstName, string LastName,
    string FatherName, int BirthDate, Gender Gender,short Nationality, bool IsDead);

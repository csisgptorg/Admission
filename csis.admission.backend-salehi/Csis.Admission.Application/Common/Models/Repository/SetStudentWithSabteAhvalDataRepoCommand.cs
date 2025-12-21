namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary>سینک دیتای ثبت احوال</summary>
public record SetStudentWithSabteAhvalDataRepoCommand(int Codm, string FirstName, string LastName, string FatherName, string BirthCertNo,
    Gender Gender, string Seri, int Serial, bool IsSadat,bool IsDead, int? DeathDate, int UserId, int PersonnelID, int ApplicationId, int RequestId, DataSource DataSource);

/// <summary>سینک دیتای ثبت احوال</summary>
public record SetDependentWithSabteAhvalDataRepoCommand(int Codm, long DependentId, string FirstName, string LastName, string FatherName, string BirthCertNo,
    Gender Gender, string Seri, int Serial, bool IsSadat, bool IsDead, int? DeathDate, int UserId, int PersonnelID, int ApplicationId, int RequestId, DataSource DataSource);

namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary>سینک دیتای المصطفی</summary>
public record SetStudentWithAlmostafaDataRepoCommand(int Codm, string FirstName, string LastName, string FatherName, string PassportNumber, Nationality Nationality,
    int ResidenceExpireDate, Gender Gender, bool IsDead, int? DeathDate, int UserId, int PersonnelID, int ApplicationId, int RequestId, DataSource DataSource);

/// <summary>سینک دیتای المصطفی</summary>
public record SetDependentWithAlmostafaDataRepoCommand(int Codm, long DependentId, string FirstName, string LastName, string FatherName, string PassportNumber,
    Nationality Nationality, int ResidenceExpireDate, Gender Gender, bool IsDead, int? DeathDate, int UserId, int PersonnelID, int ApplicationId, int RequestId,
    DataSource DataSource);

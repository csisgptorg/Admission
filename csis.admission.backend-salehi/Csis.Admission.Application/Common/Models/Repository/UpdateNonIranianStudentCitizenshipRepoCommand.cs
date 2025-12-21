namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary></summary>
public record UpdateNonIranianStudentCitizenshipRepoCommand(int Codm, string NationalCode, int BirthDate);

/// <summary></summary>
public record UpdateNonIranianDependentCitizenshipRepoCommand(long DependentId, string NationalCode, int BirthDate);

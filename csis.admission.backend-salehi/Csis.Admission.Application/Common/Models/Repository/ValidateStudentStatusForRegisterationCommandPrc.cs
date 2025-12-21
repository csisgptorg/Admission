namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary></summary>
public record ValidateStudentStatusForRegistrationCommandPrc(string NationalCode, string YektaCode, Citizenship Citizenship,
    int BirthDate, ApprovalCenter ApprovalCenter, long? CaseNumInApprovalCenter);

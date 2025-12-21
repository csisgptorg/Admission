namespace Csis.Admission.Application.Common.Dtos;

/// <summary></summary>
public class ValidateStudentStatusForRegisterationResultDto
{
    /// <summary></summary>
    public bool IsValid { get; init; }

    /// <summary></summary>
    public string Message { get; init; }

    /// <summary></summary>
    public int? Codm { get; set; }
}

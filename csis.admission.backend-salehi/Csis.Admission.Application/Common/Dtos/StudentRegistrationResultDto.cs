namespace Csis.Admission.Application.Common.Dtos;

/// <summary></summary>
public class StudentRegistrationResultDto
{
    /// <summary></summary>
    public bool IsSuccess { get; init; }

    /// <summary></summary>
    public string Message { get; init; }

    /// <summary></summary>
    public int Codm { get; init; }
}

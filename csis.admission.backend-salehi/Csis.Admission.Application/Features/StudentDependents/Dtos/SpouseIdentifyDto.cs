namespace Csis.Admission.Application.Features.StudentDependents.Dtos;

/// <summary> </summary>
public sealed record SpouseIdentifyDto
{
    /// <summary> </summary>
    public SpouseIdentifyDto(string firstName, string lastName, string fatherName, string nationalCode,
        string birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        FatherName = fatherName;
        NationalCode = nationalCode;
        BirthDate = birthDate;
    }

    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string FatherName { get; init; }
    public string NationalCode { get; init; }
    public string BirthDate { get; init; }
}

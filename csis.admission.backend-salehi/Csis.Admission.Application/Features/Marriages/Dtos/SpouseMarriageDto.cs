namespace Csis.Admission.Application.Features.Marriages.Dtos;

/// <summary>
/// مدل نمایشی مشخصات همسر
/// </summary>
public sealed record class SpouseMarriageDto
{
    /// <inheritdoc/>>
    public SpouseMarriageDto(string name, string family, string fatherName, string birthDate) {
        Name = name;
        Family = family;
        FatherName = fatherName;
        BirthDate = birthDate;
    }

    /// <inheritdoc/>
    public string Name { get; set; }

    /// <inheritdoc/>
    public string Family { get; set; }

    /// <inheritdoc/>
    public string FatherName { get; set; }

    /// <inheritdoc/>
    public string BirthDate { get; set; }
}

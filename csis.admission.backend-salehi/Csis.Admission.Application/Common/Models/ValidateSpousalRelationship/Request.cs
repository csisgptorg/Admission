using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Common.Models.ValidateSpousalRelationship;

/// <inheritdoc/>
public sealed class ValidateSpousalRelationshipRequest
{
    /// <inheritdoc/>
    public ValidateSpousalRelationshipRequest(int codm, string nationalCode, int? birthDate, string nationalCodeSpouse, string birthDateSpouse,
        string eventDate, RelationTypeEnum relationType) {
        Codm = codm;
        NationalCode = nationalCode;
        BirthDate = birthDate.Value;
        NationalCodeSpouse = nationalCodeSpouse;
        BirthDateSpouse = birthDateSpouse.StringDateToInt().Value;
        EventDate = eventDate.StringDateToInt().Value;
        RelationType = relationType;
    }

    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public string NationalCode { get; set; }

    /// <inheritdoc/>
    public int BirthDate { get; set; }

    /// <inheritdoc/>
    public string NationalCodeSpouse { get; set; }

    /// <inheritdoc/>
    public int BirthDateSpouse { get; set; }

    /// <inheritdoc/>
    public int EventDate { get; set; }

    /// <inheritdoc/>
    public RelationTypeEnum RelationType { get; set; }

    /// <inheritdoc/>
    public enum RelationTypeEnum
    {

        /// <inheritdoc/>
        Marriage = 1,

        /// <inheritdoc/>
        Divorce = 2
    }
}

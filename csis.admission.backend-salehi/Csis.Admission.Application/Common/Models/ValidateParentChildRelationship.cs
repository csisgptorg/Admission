using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Common.Models;

/// <inheritdoc/>
public sealed class ValidateParentChildRelationshipRequest
{
    /// <inheritdoc/>
    public ValidateParentChildRelationshipRequest(string parentNationalCode, string parentBirthDate, string childNationalCode, 
        string childBirthDate, RelationTypeEnum relationType) {
        ParentNationalCode = parentNationalCode;
        ParentBirthDate = parentBirthDate.StringDateToInt()!.Value;
        ChildNationalCode = childNationalCode;
        ChildBirthDate = childBirthDate.StringDateToInt().ToString();
        RelationType = relationType;
    }

    /// <inheritdoc/>
    public string ParentNationalCode { get; set; }
    /// <inheritdoc/>
    public int ParentBirthDate { get; set; }
    /// <inheritdoc/>
    public string ChildNationalCode { get; set; }
    /// <inheritdoc/>
    public string ChildBirthDate { get; set; }
    /// <inheritdoc/>
    public RelationTypeEnum RelationType { get; set; }

    /// <inheritdoc/>
    public enum RelationTypeEnum
    {
        /// <inheritdoc/>
        FatherChild = 1,
        /// <inheritdoc/>
        MotherChild = 2
    }
}

/// <inheritdoc/>
public class ValidateParentChildRelationshipResponse
{
    /// <inheritdoc/>
    public bool IsRelationFound { get; set; }
    /// <inheritdoc/>
    public bool IsPersonFound { get; set; }
    /// <inheritdoc/>
    public GetIdentityInfoByNationalCodeResponse ChildHoviatFull { get; set; }

    /// <inheritdoc/>
    public Result GetResult() {
        if ( IsRelationFound )
            return Result.ValidRelation;
        else if ( IsPersonFound )
            return Result.ValidNationalCode;
        return Result.InvalidNationalCode;
    }

    /// <inheritdoc/>
    public enum Result
    {
        /// <inheritdoc/>
        InvalidNationalCode,
        /// <inheritdoc/>
        ValidNationalCode,
        /// <inheritdoc/>
        ValidRelation
    }
}

namespace Csis.Admission.Application.Common.Models;

/// <inheritdoc/>
public class ValidateSpousalRelationshipResponse
{
    /// <inheritdoc/>
    public bool IsPersonFound { get; set; }
    /// <inheritdoc/>
    public bool IsRelationFound { get; set; }

    /// <inheritdoc/>
    public Result GetResult() {
        if( IsRelationFound )return Result.ValidRelation;
        else if( IsPersonFound )return Result.ValidNationalCode;
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

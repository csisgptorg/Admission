namespace Csis.Admission.Application.Common.Models;

/// <inheritdoc/>
public sealed class ValidateNonIranianRelationshipRequest
{
    /// <inheritdoc/>
    public ValidateNonIranianRelationshipRequest(string personYektaCode, string relatedYektaCode) {
        PersonYektaCode = personYektaCode;
        RelatedYektaCode = relatedYektaCode;
    }

    /// <inheritdoc/>
    public string PersonYektaCode { get; set; }
    /// <inheritdoc/>
    public string RelatedYektaCode { get; set; }
}


/// <inheritdoc/>
public class ValidateNonIranianRelationshipResponse
{
    /// <inheritdoc/>
    public bool IsRelationFound { get; set; }
    /// <inheritdoc/>
    public string RelationId { get; set; }
    /// <inheritdoc/>
    public string Relation { get; set; }

    /// <inheritdoc/>
    public Result GetResult() => IsRelationFound ? Result.ValidRelation : Result.InvalidYektaCode;

    public enum Result
    {
        /// <inheritdoc/>
        InvalidYektaCode,
        /// <inheritdoc/>
        ValidRelation
    }

    public enum NonIranianDependentRelation
    {
        /// <inheritdoc/>
        Spouse = 2,
        /// <inheritdoc/>
        Child = 6,
    }
}

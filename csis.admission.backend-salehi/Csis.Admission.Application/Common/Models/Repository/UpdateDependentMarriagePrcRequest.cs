using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

/// <inheritdoc/>
public class UpdateDependentMarriagePrcRequest : RepoCommandLogParam
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public long DependentId { get; set; }

    /// <inheritdoc/>
    public int MarriageDate { get; set; }
};


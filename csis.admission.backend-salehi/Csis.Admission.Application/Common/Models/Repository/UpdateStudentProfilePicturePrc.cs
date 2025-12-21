using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models;

/// <inheritdoc/>
public class UpdateStudentProfilePicturePrc : RepoCommandLogParam
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public byte[] Picture { get; set; }
};

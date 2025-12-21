using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

/// <inheritdoc/>
public class StudentDependentRegistryPrcRequest : RepoCommandLogParam
{
    /// <inheritdoc/>
    public DependentRelation Relation { get; set; }

    /// <inheritdoc/>
    public int Codm { get; set; }
    /// <inheritdoc/>
    public string NationalCode { get; set; }

    /// <inheritdoc/>
    public string FirstName { get; set; }
    /// <inheritdoc/>
    public string LastName { get; set; }
    /// <inheritdoc/>
    public string FatherName { get; set; }
    /// <inheritdoc/>
    public string MotherName { get; set; }
    /// <inheritdoc/>
    public int? BirthDate { get; set; }
    /// <inheritdoc/>
    public Gender? Gender { get; set; }
    /// <inheritdoc/>
    public Religion? Religion { get; set; }
    /// <inheritdoc/>
    public Citizenship? Citizenship { get; set; }
    /// <inheritdoc/>
    public bool IsSadat { get; set; }


    /// <inheritdoc/>
    public string BirthCertNumber { get; set; }
    /// <inheritdoc/>
    public string BirthCertSeri { get; set; }
    /// <inheritdoc/>
    public int? BirthCertSerial { get; set; }
    /// <inheritdoc/>
    public string BirthCertIssuePlace { get; set; }

    /// <inheritdoc/>
    public SingleStatus? SingleStatus { get; set; }
    /// <inheritdoc/>
    public bool IsMarried { get; set; }
    /// <inheritdoc/>
    public int? MarriageDate { get; set; }
    /// <inheritdoc/>
    public int? DivorceDate { get; set; }

    /// <inheritdoc/>
    public bool IsDead { get; set; }
    /// <inheritdoc/>
    public int? DeathDate { get; set; }

    /// <inheritdoc/>
    public string YektaCode { get; set; }

    /// <inheritdoc/>
    public int Nationality { get; set; }

    /// <inheritdoc/>
    public string? PassportNumber { get; set; }

    /// <inheritdoc/>
    public int? ResidenceExpireDate { get; set; }
    /// <inheritdoc/>
    //public string? FidaCode { get; set; }
};

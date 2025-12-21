using Csis.Admission.Application.Features.Students.Commands;

namespace Csis.Admission.Application.Common.Models.Repository;

/// <summary></summary>
public class StudentRegistrationCommandPrc:RepoCommandLogParam
{
    /// <summary></summary>
    public string FirstName { get; set; }
    
    /// <summary></summary>
    public bool IsSadat { get; set; }
    
    /// <summary></summary>
    public string LastName { get; set; }
    
    /// <summary></summary>
    public string FatherName { get; set; }
    
    /// <summary></summary>
    public int? BirthDate { get; set; }
    
    /// <summary></summary>
    public short Gender { get; set; }
    
    /// <summary></summary>
    public Religion Religion { get; set; }
    
    /// <summary></summary>
    public Citizenship Citizenship { get; set; }
    
    /// <summary></summary>
    public string NationalCode { get; set; }
    
    /// <summary></summary>
    public int BirthCertNumber { get; set; }
    
    /// <summary></summary>
    public string BirthCertSeri { get; set; }
    
    /// <summary></summary>
    public int? BirthCertSerial { get; set; }
    
    /// <summary></summary>
    public string BirthCertIssuePlace { get; set; }  
    
    /// <summary></summary>
    public string BirthCertDescription { get; set; }
    
    /// <summary></summary>
    public short Nationality { get; set; }
    
    /// <summary></summary>
    public string PassportNumber { get; set; }
    
    /// <summary></summary>
    public string YektaCode { get; set; }
    
    /// <summary></summary>
    public int? ResidenceExpireDate { get; set; }
    
    /// <summary></summary>
    public bool IsMarried { get; set; }
    
    /// <summary></summary>
    public int? MarriageDate { get; set; }
    
    /// <summary></summary>
    public int? DivorceDate { get; set; }

    /// <summary></summary>
    public short SingleStatus { get; set; }
    
    /// <summary></summary>
    public bool IsDead { get; set; }
    
    /// <summary></summary>
    public int? DeathDate { get; set; }
 
    /// <summary></summary>
    public int CaseCreationDate { get; set; }
    
    /// <summary></summary>
    public bool IsActive { get; set; }
    
    /// <summary></summary>
    public int? CaseValidityDate { get; set; }
    
    /// <summary></summary>
    public bool IsBlock { get; set; }
    
    /// <summary></summary>
    public int BlockDate { get; set; }
    
    /// <summary></summary>
    public ApprovalCenter ApprovalCenter { get; set; }

    /// <summary>شماره پرونده در مرجع تایید کننده</summary>
    public long? CaseNumInApprovalCenter { get; set; }

    /// <summary></summary>
    public int CommissionRequestId { get; set; }
}


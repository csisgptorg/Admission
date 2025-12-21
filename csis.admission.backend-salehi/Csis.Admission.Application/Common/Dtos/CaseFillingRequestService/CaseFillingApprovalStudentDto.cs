namespace Csis.Admission.Application.Common.Dtos.RequestService;
/// <inheritdoc/>
public class CaseFillingApprovalStudentDto
{
    /// <inheritdoc/>
    public long RequestId { get; set; }
    /// <inheritdoc/>
    public int ApproverCodm { get; set; }
    /// <inheritdoc/>
    public ApprovalStatus Status { get; set; }
}


namespace Csis.Admission.Application.Features.CaseFilings.Dtos;

/// <summary>
/// گام اول ساخت پرونده پذیرش طلبه
/// </summary>
public sealed record AdmissionCaseStepDto(Guid Token, AdmissionCaseStep CaseStep);

namespace Csis.Admission.Application.Features.CaseFilings.Dtos;

/// <summary>
/// گام اول ساخت پرونده پذیرش طلبه
/// </summary>
public sealed record CreateAdmissionCaseStepOneDto(Guid Token, string Mobile);

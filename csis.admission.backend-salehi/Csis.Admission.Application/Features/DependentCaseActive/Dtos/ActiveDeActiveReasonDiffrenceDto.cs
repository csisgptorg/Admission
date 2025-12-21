namespace Csis.Admission.Application.Features.DependentCaseActive.Commands;

/// <summary>
/// تفاوت علت فعال یا غیر فعال بودن تکفل
/// </summary>
/// <param name="PreviousReason"></param>
/// <param name="NewReason"></param>
public sealed record ActiveDeActiveReasonDiffrenceDto(DependentActiveDeactiveReason PreviousReason, DependentActiveDeactiveReason NewReason);

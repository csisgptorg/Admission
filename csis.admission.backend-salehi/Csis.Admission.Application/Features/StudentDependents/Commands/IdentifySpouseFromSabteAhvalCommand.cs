using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.StudentDependents.Dtos;
using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.StudentDependents.Commands;

/// <summary>
/// فرمان دریافت اطلاعات هویتی از ثبت احوال و ثبت همسر
/// </summary>
public sealed record IdentifySpouseFromSabteAhvalCommand : IRequest<SpouseIdentifyDto>
{
    /// <summary>کد مرکز دانشجو</summary>
    public int? Codm { get; set; }
    /// <summary>کد ملی همسر</summary>
    public string SpouseNationalCode { get; init; }

    /// <summary>تاریخ تولد همسر</summary>
    public string SpouseBirthDate { get; init; }

    /// <summary>تاریخ ازدواج</summary>
    public string MarriageDate { get; init; }

    /// <summary>مذهب همسر</summary>
    public Religion Religion { get; init; }
}

internal sealed class RegisterSpouseFromSabteAhvalCommandHandler(ICsisWsmService csisWsmService, ICsisAuthenticatedUserService authenticatedUserService)
    : IRequestHandler<IdentifySpouseFromSabteAhvalCommand, SpouseIdentifyDto>
{
    public async Task<SpouseIdentifyDto> Handle(IdentifySpouseFromSabteAhvalCommand command, CancellationToken cancellationToken) {

        var request = new GetIdentityInfoByNationalCodeRequest(
           command.Codm ?? -1,
           command.SpouseNationalCode,
           command.SpouseBirthDate.StringDateToInt().Value);

        var hoviat = await csisWsmService.GetIdentityInfoByNationalCode(request, cancellationToken);
        return new SpouseIdentifyDto(hoviat.Name, hoviat.Family, hoviat.FatherName, hoviat.Nin, hoviat.BirthDate);
    }
}

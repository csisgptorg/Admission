using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Interfaces;

namespace Csis.Admission.Services;

/// <summary>سرویس اطلاعات کاربر جاری</summary>
internal sealed class BirthCertService(ICsisWsmService wsmService): IBirthCertService
{
    public async Task<BirthCertInfo> Iranian(string nationalCode, string birthDate, CancellationToken cancellation) {
        var request = new GetIdentityInfoByNationalCodeRequestApiM(nationalCode, birthDate);
        var identityInfo = await wsmService.GetIdentityInfoByNationalCode(request, cancellation);
        if ( string.IsNullOrEmpty(identityInfo.Nin) ) {
            throw new CommandValidationException("کد ملی یا تاریخ تولد وارد شده در سامانه ثبت احوال یافت نشد.");
        }
        return identityInfo.BirthCertInfo();
    }

    public async Task<NonIranianBirthCertInfo> NonIranian(string yektaCode, CancellationToken cancellation) {
        var identityInfo = await wsmService.GetIdentityInfoByYektaCode(yektaCode, cancellation);
        if ( string.IsNullOrWhiteSpace(identityInfo.YektaCode) ) {
            throw new CommandValidationException(nameof(identityInfo), "کد یکتا در سامانه المصطفی یافت نشد.");
        }
        return identityInfo.BirthCertInfo();
    }
}

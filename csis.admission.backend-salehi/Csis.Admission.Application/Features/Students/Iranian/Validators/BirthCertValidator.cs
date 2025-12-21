using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Students.Validators;

internal sealed class BirthCertValidator(IRepository<StudentSummary> studentSummaryRepo, IRepository<DependentSummary, long> dependentSummaryRepo, ICsisWsmService wsmService)
{
    public async Task<BirthCertInfo> DependentIdentityIranian(long id, string nationalCode, string birthDate, CancellationToken cancellation) {
        // Age
        var age = Common.Utilities.CalculateAge(birthDate, null);
        if ( age > 100 ) { throw new CommandValidationException("سن شما بیش از ۱۰۰ سال است. لطفاً تاریخ تولد خود را بررسی کرده و در صورت نیاز اصلاح نمایید."); }

        // duplicate nationalCode
        var nationalExists = await dependentSummaryRepo.ExistsAsync(x => x.Id != id && x.NationalCode == nationalCode, false, cancellation);
        if ( nationalExists ) { throw new CommandValidationException("کد ملی واردشده قبلاً در سامانه ثبت شده است."); }

        // validate with wsm
        var identityRequest = new GetIdentityInfoByNationalCodeRequest(-1, nationalCode, birthDate.StringDateToInt().Value);
        var identityInfo = await wsmService.GetIdentityInfoByNationalCode(identityRequest, cancellation);
        if ( string.IsNullOrEmpty(identityInfo.Nin) ) {
            throw new CommandValidationException("کد ملی یا تاریخ تولد وارد شده در ثبت احوال یافت نشد.");
        }

        return identityInfo.BirthCertInfo();
    }
}

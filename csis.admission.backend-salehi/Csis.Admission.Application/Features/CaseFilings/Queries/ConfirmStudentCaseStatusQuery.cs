using Csis.Admission.Application.Common.Services;
using Csis.Admission.Application.Features.CaseFilings.Dtos;

namespace Csis.Admission.Application.Features.CaseFilings.Queries;

/// <summary> تأیید مرحله وضعیت پرونده دانشجویی </summary>
public sealed record ConfirmStudentCaseStatusQuery(string Identity, string Otp) : IRequest<AdmissionCaseUserDto>;

internal sealed class ConfirmStudentCaseProgressQueryHandler(
    IRepository<AdmissionCaseUser, Guid> caseRepository,
    IMemoryCacheService memoryCacheService,
    IOtpSenderService otpSenderService)
    : IRequestHandler<ConfirmStudentCaseStatusQuery, AdmissionCaseUserDto>
{
    public async Task<AdmissionCaseUserDto> Handle(ConfirmStudentCaseStatusQuery request, CancellationToken cancellationToken) {
        var admissionCaseUser = await caseRepository.GetOneAsync<AdmissionCaseUserDto>(x => x.NationalCode == request.Identity || x.YektaCode == request.Identity, cancellationToken: cancellationToken)
                                ?? throw new CommandValidationException("شناسه نامعتبر است.");

            var cachedMobile = memoryCacheService.Get<string>(request.Identity);

            var verifiedOtp = await otpSenderService.VerifyOtpAsync(
                request.Otp, cachedMobile, nameof(GetStudentCaseStatusQuery), cancellationToken);

            if ( !verifiedOtp ) {
                throw new CommandValidationException("کد تایید موبایل نامعتبر است.");
            }

            return admissionCaseUser;
    }
}

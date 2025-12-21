using Microsoft.AspNetCore.Http;
using Csis.Admission.Application.Common.Services;
using Csis.Admission.Application.Features.CaseFilings.Dtos;

namespace Csis.Admission.Application.Features.CaseFilings.Commands;

/// <summary>تایید موبایل گام دوم</summary>
public sealed record CreateAdmissionCaseSecondStepCommand(Guid Token, string Otp) : IRequest<CreateAdmissionCaseSecondStepDto>;

internal sealed class ConfirmMobileCommandHandler(
    IOtpSenderService otpSenderService,
    IHttpContextAccessor contextAccessor,
    IMemoryCacheService memoryCacheService,
    IRepository<AdmissionCaseUser, Guid> caseUserRepo,
    IMapper mapper)
    : IRequestHandler<CreateAdmissionCaseSecondStepCommand, CreateAdmissionCaseSecondStepDto>
{
    public async Task<CreateAdmissionCaseSecondStepDto> Handle(CreateAdmissionCaseSecondStepCommand request, CancellationToken cancellationToken) {
        var admissionCaseUser = await caseUserRepo.GetByIdAsTrackingAsync(request.Token, cancellationToken: cancellationToken)
                                ?? throw new CommandValidationException("شناسه نامعتبر است.");
        if ( admissionCaseUser.ConfirmMobile != true ) {
            #region TEST

            //if ( request.Otp == "1234" ) {
            //    admissionCaseUser.ConfirmMobile = true;
            //    admissionCaseUser.CaseStep = AdmissionCaseStep.MobileVerified;
            //    await caseUserRepo.UpdateAsync(admissionCaseUser, cancellationToken: cancellationToken);
            //    memoryCacheService.Remove(request.Token.ToString());

            //    return admissionCaseUser.Id;
            //}

            #endregion
            var cachedMobile = memoryCacheService.Get<string>(request.Token.ToString());

            var verifiedOtp = await otpSenderService.VerifyOtpAsync(
                request.Otp, cachedMobile, nameof(CreateAdmissionCaseFirstStepCommand), cancellationToken);
            if ( Common.Utilities.IsDevMode(contextAccessor.HttpContext) ) {
                verifiedOtp = true;
            }

            if ( !verifiedOtp ) {
                throw new CommandValidationException("کد تایید موبایل نامعتبر است.");
            }

            admissionCaseUser.ConfirmMobile = true;
            admissionCaseUser.CaseStep = AdmissionCaseStep.MobileVerified;
            await caseUserRepo.UpdateAsync(admissionCaseUser, cancellationToken: cancellationToken);
            memoryCacheService.Remove(request.Token.ToString());
        }
        return new CreateAdmissionCaseSecondStepDto(admissionCaseUser.Id, admissionCaseUser.CaseStep);
    }
}

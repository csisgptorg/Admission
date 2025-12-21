using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Common.Services;
using Csis.Admission.Application.Features.CaseFilings.Dtos;

namespace Csis.Admission.Application.Features.CaseFilings.Queries;


/// <summary> وضعیت پرونده دانشجویی </summary>
public sealed record GetStudentCaseStatusQuery : IRequest<AdmissionCaseStatusDto>
{
    public string? NationalCode { get; init; }
    public string? YektaCode { get; init; }
    public int BirthDate { get; init; }
    public string Mobile { get; init; }
    public string CaptchaToken { get; init; }
    public string CaptchaCode { get; init; }

}
internal sealed class GetStudentCaseStatusQueryHandler(
    IRepository<AdmissionCaseUser, Guid> caseRepository,
    IMemoryCacheService memoryCacheService,
    IOtpSenderService otpSenderService)
    : IRequestHandler<GetStudentCaseStatusQuery, AdmissionCaseStatusDto>
{
    public async Task<AdmissionCaseStatusDto> Handle(GetStudentCaseStatusQuery request, CancellationToken cancellationToken) {
        var storedCaptcha = memoryCacheService.Get<string>(request.CaptchaToken);

        if ( storedCaptcha != request.CaptchaCode ) {
            throw new CommandValidationException(nameof(request.CaptchaCode), "کد کپچا اشتباه است");
        }
        memoryCacheService.Remove(request.CaptchaToken);


        var userCase = await caseRepository.GetOneAsync(x =>
            (x.NationalCode == request.NationalCode || x.YektaCode == request.YektaCode) &&
            x.BirthDate == request.BirthDate && x.Mobile == request.Mobile
            && x.CaseStep >= AdmissionCaseStep.MobileVerified && x.CaseStep <= AdmissionCaseStep.RegistrationCompleted
        , cancellationToken: cancellationToken);

        if ( userCase is not null ) {
            await otpSenderService.SendOtpAsync(request.Mobile, request.GetType().Name, cancellationToken);
            memoryCacheService.Set(userCase.NationalCode ?? userCase.YektaCode, request.Mobile,
                new CacheOptions { AbsoluteExpirationSeconds = 120 });

            return new AdmissionCaseStatusDto(identity: userCase.NationalCode ?? userCase.YektaCode, mobile: MobileWithAsterics(request.Mobile));
        }

        return null;
    }

    private string MobileWithAsterics(string mobile) => mobile.Substring(0, 3) + "****" + mobile.Substring(7);
}

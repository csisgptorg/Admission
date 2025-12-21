using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Services;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.CaseFilings.Dtos;
using Csis.Admission.Application.Features.CaseFilings.Validator;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Csis.Admission.Application.Features.CaseFilings.Commands;

/// <summary>ساخت توکن گام اول</summary>
public sealed record CreateAdmissionCaseFirstStepCommand :
    BaseCommandDto<CreateAdmissionCaseFirstStepCommand, AdmissionCaseUser, Guid>, IRequest<CreateAdmissionCaseStepOneDto>
{
    /// <summary></summary>
    public string NationalCode { get; init; }

    /// <summary></summary>
    public string YektaCode { get; init; }

    /// <summary></summary>
    public Citizenship Citizenship { get; init; }

    /// <summary></summary>
    public string BirthDate { get; init; }

    /// <summary></summary>
    public string Mobile { get; init; }

    /// <summary></summary>
    public string CaptchaToken { get; init; }

    /// <summary></summary>
    public string CaptchaCode { get; init; }

    /// <inheritdoc/>
    public override void ReverseCustomMappings(IMappingExpression<CreateAdmissionCaseFirstStepCommand, AdmissionCaseUser> mapping) {
        base.ReverseCustomMappings(mapping);
        mapping.ForMember(x => x.BirthDate, opt => opt.MapFrom(x => x.BirthDate.StringDateToInt()));
    }
}

internal sealed class CreateAdmissionCaseCommandHandler(
    IdentityValidator identityValidator,
    IOtpSenderService otpSenderService,
    IHttpContextAccessor contextAccessor,
    IMemoryCacheService memoryCacheService,
    IHttpContextAccessor httpContextAccessor,
    IRepository<AdmissionCaseUser, Guid> requestRepo,
    ILogger<CreateAdmissionCaseCommandHandler> logger)
    : IRequestHandler<CreateAdmissionCaseFirstStepCommand, CreateAdmissionCaseStepOneDto>
{
    public async Task<CreateAdmissionCaseStepOneDto> Handle(CreateAdmissionCaseFirstStepCommand command, CancellationToken cancellationToken) {
        CheckCaptcha(command);

        if( command.Citizenship == Citizenship.Iranian ) {
            await identityValidator.Iranian(command.NationalCode, command.BirthDate, command.Mobile, cancellationToken);
        } else {
            await identityValidator.NonIranian(command.YektaCode, command.BirthDate, cancellationToken);
        }
            

        var otpType = command.GetType().Name + (Common.Utilities.IsDevMode(contextAccessor.HttpContext) ? Guid.NewGuid() : "");
        await otpSenderService.SendOtpAsync(command.Mobile, otpType, cancellationToken);

        var caseId=await CreateOrUpdateCase(command,cancellationToken);
        return new CreateAdmissionCaseStepOneDto(caseId, command.Mobile);
    }

    private void CheckCaptcha(CreateAdmissionCaseFirstStepCommand command) {
        var storedCaptcha = memoryCacheService.Get<string>(command.CaptchaToken);
        logger.LogInformation("storedCaptcha : {StoredCaptcha}", storedCaptcha);
        if ( storedCaptcha != command.CaptchaCode && !Common.Utilities.IsDevMode(httpContextAccessor.HttpContext) ) {
            throw new CommandValidationException(nameof(command.CaptchaCode), "کد کپچا اشتباه است");
        }

        memoryCacheService.Remove(command.CaptchaToken);
    }

    private async Task<Guid> CreateOrUpdateCase(CreateAdmissionCaseFirstStepCommand command, CancellationToken cancellationToken) {
        var admissionCaseUser = await requestRepo.GetOneAsTrackingAsync(x =>
            (command.NationalCode != null && x.NationalCode == command.NationalCode) ||
            (command.YektaCode != null && x.YektaCode == command.YektaCode), false, cancellationToken);

        if ( admissionCaseUser == null ) {
            admissionCaseUser = command.ToEntity();
            await requestRepo.InsertAsync(admissionCaseUser, true, cancellationToken);

        } else {
            admissionCaseUser = command.ToEntity(admissionCaseUser);
            await requestRepo.UpdateAsync(admissionCaseUser, true, cancellationToken);
        }

        memoryCacheService.Set(admissionCaseUser.Id.ToString(), command.Mobile, new CacheOptions { AbsoluteExpirationSeconds = 120 });
        return admissionCaseUser.Id;
    }
}

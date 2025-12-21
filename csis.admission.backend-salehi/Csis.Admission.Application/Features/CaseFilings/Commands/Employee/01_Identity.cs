using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Helpers;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.CaseFilings.Dtos;
using Csis.Admission.Application.Features.CaseFilings.Validator;
using Csis.Shared.Kernel.Public.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Csis.Admission.Application.Features.CaseFilings.Commands;

/// <summary>ثبت اطلاعات هویتی</summary>
public sealed record CreateAdmissionCaseIdentityByEmployeeCommand
    : BaseCommandDto<CreateAdmissionCaseIdentityByEmployeeCommand, AdmissionCaseUser, Guid>, IRequest<AdmissionCaseStepDto>
{
    /// <summary>مرکز تایید کننده</summary>
    public ApprovalCenter ApprovalCenter { get; init; }

    /// <summary>شماره پرونده در مرکز تایید کننده</summary>
    public int ApprovalCenterCaseId { get; init; }

    /// <summary>مذهب</summary>
    public Religion Religion { get; init; }


    /// <summary>کد ملی</summary>
    public string NationalCode { get; init; }

    /// <summary>کد یکتا</summary>
    public string YektaCode { get; init; }

    /// <summary>تابعیت</summary>
    public Citizenship Citizenship { get; init; }

    /// <summary>تاریخ تولد</summary>
    public string BirthDate { get; init; }

    /// <summary>شماره موبایل</summary>
    public string Mobile { get; init; }

    /// <summary>تایید</summary>
    public bool? Confirmed { get; set; }

    /// <inheritdoc/>
    public override void ReverseCustomMappings(IMappingExpression<CreateAdmissionCaseIdentityByEmployeeCommand, AdmissionCaseUser> mapping) {
        base.ReverseCustomMappings(mapping);
        mapping.ForMember(x => x.BirthDate, opt => opt.MapFrom(x => x.BirthDate.StringDateToInt()));
        mapping.ForMember(x => x.CaseNumInApprovalCenter, opt => opt.MapFrom(x => x.ApprovalCenterCaseId));
        mapping.ForMember(x => x.ConfirmIdentityInformation, opt => opt.MapFrom(x => true));
        mapping.ForMember(x => x.ConfirmMobile, opt => opt.MapFrom(x => true));
    }
}

internal sealed class CreateAdmissionCaseIdentityByEmployeeCommandHandler(ICsisWsmService wsmService, IStudentRepository studentRepo,
    IRepository<AdmissionCaseUser, Guid> repo, IdentityValidator identityValidator,ApprovalCenterValidator approvalCenterValidator) :
    IRequestHandler<CreateAdmissionCaseIdentityByEmployeeCommand, AdmissionCaseStepDto>
{
    public async Task<AdmissionCaseStepDto> Handle(CreateAdmissionCaseIdentityByEmployeeCommand command, CancellationToken cancellation) {

        if(command.Citizenship == Citizenship.Iranian ) {
            await approvalCenterValidator.Iranian(command.ApprovalCenterCaseId, command.ApprovalCenter, command.NationalCode, command.BirthDate, cancellation);
            await identityValidator.Iranian(command.NationalCode, command.BirthDate, command.Mobile, cancellation);

        } else {
            await approvalCenterValidator.NonIranian(command.ApprovalCenterCaseId, command.ApprovalCenter, command.YektaCode, command.BirthDate, cancellation);
            await identityValidator.NonIranian(command.YektaCode, command.BirthDate, cancellation);
        }
        

        var admissionCase = await repo.GetOneAsTrackingAsync(x =>
            (command.NationalCode != null && x.NationalCode == command.NationalCode) ||
            (command.YektaCode != null && x.YektaCode == command.YektaCode), false, cancellation);
        if ( admissionCase == null ) {
            admissionCase = command.ToEntity();

            if ( command.Confirmed != true ) {
                await Iranian(command, admissionCase, false, cancellation);
                await NonIranian(command, admissionCase, false, cancellation);
            }
            await Iranian(command, admissionCase, true, cancellation);
            await NonIranian(command, admissionCase, true, cancellation);

            admissionCase.CaseStep = AdmissionCaseStep.IdentityVerified;
            await repo.InsertAsync(admissionCase, true, cancellation);
        }

        return new AdmissionCaseStepDto(admissionCase.Id, admissionCase.CaseStep.Value);
    }

    private async Task Iranian(CreateAdmissionCaseIdentityByEmployeeCommand command, AdmissionCaseUser admissionCaseUser, bool isConfirmed, CancellationToken cancellationToken) {
        if ( command.Citizenship != Citizenship.Iranian ) {
            return;
        }

        var validateMobileRequest = new ValidateMobileOwnershipRequest(command.NationalCode,
            command.Mobile, ValidateMobileOwnershipRequest.IdentityType.NationalCode);
        if ( !await wsmService.ValidateMobileOwnership(validateMobileRequest, cancellationToken) ) {
            throw new CommandValidationException("شماره موبایل با کد ملی تطابق ندارد.");
        }

        var validation = new Common.Models.Repository.ValidateStudentStatusForRegistrationCommandPrc(
            command.NationalCode,
            command.YektaCode,
            command.Citizenship,
            command.BirthDate.StringDateToInt().Value,
            command.ApprovalCenter,
            command.ApprovalCenterCaseId);
        var validateStudent = await studentRepo.ValidateStudentStatusForRegistration(validation);
        if ( !validateStudent.IsValid && false ) {
            throw new CommandValidationException(validateStudent.Message);
        }

        var request = new GetIdentityInfoByNationalCodeRequest(-1, command.NationalCode, command.BirthDate.StringDateToInt().Value);
        var identityInfo = await wsmService.GetIdentityInfoByNationalCode(request, cancellationToken);
        if ( string.IsNullOrEmpty(identityInfo.Nin) ) {
            throw new CommandValidationException(nameof(identityInfo), "کد ملی وارد شده نامعتبر است");
        }

        var gender = (Gender) Enum.Parse(typeof(Gender), identityInfo.Gender);
        var info = new ValidateIdentityDto(identityInfo.Name, identityInfo.Family, identityInfo.FatherName,
                    Citizenship.Iranian, identityInfo.BirthDate, command.NationalCode, Gender: gender);

        admissionCaseUser.Payloads = PayloadHelper.AddPayloadsToString(info, admissionCaseUser.Payloads,
            nameof(AdmissionCasePayloadName.Identity));

        await repo.UpdateAsync(admissionCaseUser, true, cancellationToken);
        if ( !isConfirmed )
            throw new ConfirmedValidationException(info);
    }

    private async Task NonIranian(CreateAdmissionCaseIdentityByEmployeeCommand command, AdmissionCaseUser admissionCaseUser, bool isConfirmed, CancellationToken cancellationToken) {
        if ( command.Citizenship != Citizenship.NonIranian ) {
            return;
        }

        var identityInfo = await wsmService.ValidateNonIranianYektaCode(-1, command.YektaCode, cancellationToken);
        if ( !identityInfo.IsValid() ) {
            throw new CommandValidationException(nameof(identityInfo), "کد یکتا وارد شده نامعتبر است");
        }

        var info = new ValidateIdentityDto(identityInfo.FirstName, identityInfo.LastName, identityInfo.FatherName, Citizenship.NonIranian,
                    identityInfo.BirthDate.ToPersianDateOnly(), YektaCode: command.YektaCode);

        admissionCaseUser.Payloads = PayloadHelper.AddPayloadsToString(info, admissionCaseUser.Payloads,
            nameof(AdmissionCasePayloadName.Identity));

        await repo.UpdateAsync(admissionCaseUser, true, cancellationToken);

        if ( !isConfirmed )
            throw new ConfirmedValidationException(info);
    }
}

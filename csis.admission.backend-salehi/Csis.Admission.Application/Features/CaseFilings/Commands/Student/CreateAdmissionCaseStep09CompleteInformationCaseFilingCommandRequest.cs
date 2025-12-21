using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Common.Dtos.RequestService;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Features.CaseFilings.Dtos;
using Csis.Authorization.Services;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Csis.Admission.Application.Features.CaseFilings.Commands;

public sealed record CompleteInformationCaseFilingCommandRequest(Guid Token) : IRequest<AdmissionCaseStatusResultDto>;

internal sealed class CompleteInformationCaseFilingCommandRequestHandler(
    IStudentRepository studentRepository,
    IEmployeeDataService employeeService,
    ICsisAuthenticatedUserService authenticatedUser,
    IRepository<StudentSummary> studentSummaryRepository,
    IOptions<ImageAnalysisOption> options,
    ICsisWsmService csisWsmService,
    IRepository<AdmissionCaseUser, Guid> toDtoRepo,
    ICaseFillingRequestService requestService,
    IMapper mapper,
    IMediator mediator)
    : IRequestHandler<CompleteInformationCaseFilingCommandRequest, AdmissionCaseStatusResultDto>
{

    private bool _isEligible;
    public async Task<AdmissionCaseStatusResultDto> Handle(CompleteInformationCaseFilingCommandRequest request,
        CancellationToken cancellationToken) {
        var admissionCaseUser = await toDtoRepo.GetByIdAsTrackingAsync(request.Token, cancellationToken: cancellationToken)
            ?? throw new CommandValidationException("شناسه نامعتبر است");

        var toDto = mapper.Map<AdmissionCaseUserDto>(admissionCaseUser);

        if ( admissionCaseUser.CaseStep >= AdmissionCaseStep.EmploymentVerified ) {
            var flow = await FlowSelector(toDto, cancellationToken);

            var requestCommand = new CreateCaseFillingRequestCommand(new { caseUser = toDto, toDto.Codm, flow }, flow, nameof(CompleteInformationCaseFilingCommand), RequestType.CompleteInformationCaseFiling);

            var result = await requestService.Create(requestCommand, cancellationToken);

            admissionCaseUser.RequestId = result;

            await toDtoRepo.UpdateAsync(admissionCaseUser, true, cancellationToken);

            if ( flow is RequestFlow.DirectRegistration ) {
                var currentUser = await CurrentEmployee();
                var educationPredicate = await IsEducationStudent(toDto, cancellationToken);

                if ( currentUser is not null && !currentUser.IsSenior ) {
                    if ( !educationPredicate ) {
                        return new AdmissionCaseStatusResultDto(null, "اطلاعات شما با موفقیت ثبت شد و پس از بررسی ، نتیجه از طریق پیامک اطلاع رسانی خواهد شد");
                    }
                }

                if ( (currentUser is not null && currentUser.IsSenior) || educationPredicate || _isEligible ) {
                    return await CreateAdmissionCaseStatusResultDto(toDto, cancellationToken);
                }
            }

            return new AdmissionCaseStatusResultDto(null, "اطلاعات شما با موفقیت ثبت شد و پس از بررسی، نتیجه از طریق پیامک اطلاع رسانی خواهد شد");
        }

        throw new CommandValidationException("اطلاعات پرونده به درستی تکمیل نشده است");
    }

    /// <summary>
    /// انتخاب مسیر جریان کاری بر اساس نوع کاربر
    /// </summary>
    /// <param name="caseUser"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<RequestFlow> FlowSelector(AdmissionCaseUserDto caseUser, CancellationToken cancellationToken) {
        var currentUser = await CurrentEmployee();

        if ( currentUser != null ) {
            if ( currentUser.IsSenior || (currentUser.PersonnelId != null && await IsEducationStudent(caseUser, cancellationToken)) ) {
                return RequestFlow.DirectRegistration;
            }

            return RequestFlow.EmployeeToSeniorEmployee;
        }


        if ( caseUser.Citizenship == Citizenship.NonIranian ) { return RequestFlow.StudentToEmployee; }

        var identityRequest = new GetIdentityInfoByNationalCodeRequest(-1, caseUser.NationalCode, caseUser.BirthDate.Value);
        var identityInfo = await csisWsmService.GetIdentityInfoByNationalCode(identityRequest, cancellationToken);
        var studentShenasnameImage = identityInfo?.Images?.LastOrDefault(x=>!string.IsNullOrEmpty(x.Image))?.Image;


        if ( string.IsNullOrEmpty(studentShenasnameImage) ) {
            return RequestFlow.StudentToEmployee;
        }

        var payload = caseUser.Payloads.First(x => x.Name == nameof(AdmissionCasePayloadName.Picture));
        var jsonElement = (JsonElement) payload.Payload;
        var isDtoAvailable = jsonElement.TryGetProperty("ImageAnalysisResultDto", out var imageAnalysisResultDto);
        double? aiResultValue = 0;
        if ( isDtoAvailable ) {

            var isAiResultAvailable = imageAnalysisResultDto.TryGetProperty("ai_percent", out var aiResult);
            if ( isAiResultAvailable ) {
                aiResultValue = aiResult.GetDouble();
            }
        }

        _isEligible = caseUser.Citizenship == Citizenship.Iranian && (aiResultValue.HasValue && aiResultValue.Value >= options.Value.AiPercent);
        return _isEligible ? RequestFlow.DirectRegistration : RequestFlow.StudentToEmployee;

    }

    private async Task<AdmissionCaseStatusResultDto> CreateAdmissionCaseStatusResultDto(AdmissionCaseUserDto caseUser, CancellationToken cancellationToken) {
        var student = new StudentSummary();
        if ( caseUser.Citizenship == Citizenship.Iranian ) {
            student = await studentSummaryRepository.GetOneAsync(x => x.NationalCode == caseUser.NationalCode,
                     cancellationToken: cancellationToken);
        } else {
            student = await studentSummaryRepository.GetOneAsync(x => x.YektaCode == caseUser.YektaCode,
                     cancellationToken: cancellationToken);
        }

        return new AdmissionCaseStatusResultDto(student.Codm, "کد مرکز شما با موفقیت ساخته شد");
    }

    private async Task<CaseFillingEmployeeDto> CurrentEmployee() {
        var personnelId = await authenticatedUser.GetPersonnelIdAsync();
        if ( !personnelId.HasValue ) {
            return null;
        }

        try {
            var employee = await employeeService.GetEmployeeInfoAsync(personnelId.Value);
            var fullName = employee != null ? employee.FirstName + " " + employee.LastName : "فاقد مشخصات";
            var isSenior = await authenticatedUser.IsAuthorizedToAsync(PermissionsEnum.SeniorPersonnel);
            return new CaseFillingEmployeeDto(personnelId.Value, fullName, isSenior);
        } catch ( Exception e ) {
            return new CaseFillingEmployeeDto(personnelId.Value, "فاقد مشخصات", false);
        }

    }

    private async Task<bool> IsEducationStudent(AdmissionCaseUserDto caseUser, CancellationToken cancellationToken) {
        var getStatus = new Common.Models.Repository.StudentEducationStatusRepoQuery(caseUser.ApprovalCenter.Value, caseUser.CaseNumInApprovalCenter.Value);
        var status = await studentRepository.GetStudentEducationStatus(getStatus);
        if ( status != null ) {
            return status.EducationStatus == EducationStatus.Student;
        }
        return false;
    }
}

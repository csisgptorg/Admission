using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Common.Models.ValidateSpousalRelationship;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.Students.Dtos;

namespace Csis.Admission.Application.Features.StudentDependents.Commands;

/// <summary>
/// درخواست ثبت همسر
/// </summary>
public record StudentSpouseRegistryRequestCommand : IRequest
{
    public int Codm { get; set; }
    public string SpouseNationalCode { get; init; }
    public string SpouseBirthDate { get; init; }
    public string MarriageDate { get; init; }
    public Religion Religion { get; init; }
}

internal sealed class StudentSpouseRegistryRequestCommandHandler(ICsisWsmService csisWsmService,
    IStudentRepository studentRepository,
    IRepository<DependentSummary, long> dependentRepository,
    ICurrentUserService currentUser,
    IRequestService requestService)
    : IRequestHandler<StudentSpouseRegistryRequestCommand>
{
    public async Task Handle(StudentSpouseRegistryRequestCommand command, CancellationToken cancellationToken) {
        _ = await Common.Utilities.SetCodm(command, currentUser);

        var student = await studentRepository.GetStudentInfoByCodm(command.Codm);

        // بررسی وجود تکفل با کد ملی همسر
        var existingDependents = await dependentRepository.GetAllAsync(
            x => x.NationalCode == command.SpouseNationalCode,
            cancellationToken: cancellationToken);

        // اگر تحت تکفل همین Codm بود خطا
        if ( existingDependents.Any(x => x.Codm == command.Codm) ) {
            throw new CommandValidationException("این فرد قبلاً ثبت شده است.");
        }

        //  بررسی شرایط دیگر
        if ( existingDependents.Any(x =>
            x.IsActive || // فعال بود خطا
            (x.Relation == DependentRelation.Spouse && x.IsMarried) || // همسر + متاهل خطا
            (x.Relation == DependentRelation.Child && !x.IsMarried && x.Gender == Gender.Female) || // دختر + مجرد خطا
            (x.Relation != DependentRelation.Spouse && (x.Relation != DependentRelation.Child && x.Gender != Gender.Female)) // نه همسر نه دختر خطا
        ) ) {
            throw new CommandValidationException("این فرد قبلاً به‌عنوان تکفل برای کُد دیگری ثبت شده است.");
        }

        if ( student.Gender == Gender.Female ) {
            throw new CommandValidationException("در سامانه سخا امکان ثبت همسر برای طلبه خواهر وجود نداد.");
        }

        if ( student.Citizenship != Citizenship.Iranian ) {
            throw new CommandValidationException("در سامانه سخا امکان ثبت اعضای خانواده برای طلاب غیر ایرانی وجود ندارد.");
        }

        // بررسی کد ملی و نسبت در ثبت احوال
        await SabteAhvalSpousalRelation(student, command, cancellationToken);
        var prcRequest = await SabteAhvalHoviat(command.Codm, command, cancellationToken);

        var spouseRegistryCommand =
            new StudentSpouseRegistryCommand { StudentDependentRegistryPrcRequest = prcRequest, Codm = command.Codm };

        var requestCommand = new CreateRequestCommand(spouseRegistryCommand, RequestFlow.DirectRegistration);
        await requestService.Create(requestCommand, cancellationToken);
    }

    private async Task SabteAhvalSpousalRelation(StudentInfoDto student, StudentSpouseRegistryRequestCommand command, CancellationToken cancellationToken) {

        var request = new ValidateSpousalRelationshipRequest(student.Codm, student.NationalCode, student.BirthDate.StringDateToInt(),
            command.SpouseNationalCode, command.SpouseBirthDate, command.MarriageDate, ValidateSpousalRelationshipRequest.RelationTypeEnum.Marriage);

        var result = await csisWsmService.ValidateSpousalRelationship(request, cancellationToken);

        if ( result == ValidateSpousalRelationshipResponse.Result.InvalidNationalCode ) {
            throw new CommandValidationException("کد ملی نامعتبر است.");
        }

        if ( result != ValidateSpousalRelationshipResponse.Result.ValidRelation ) {
            throw new CommandValidationException("نسبت همسر در ثبت احوال ثبت نشده است.");
        }
    }

    private async Task<StudentDependentRegistryPrcRequest> SabteAhvalHoviat(int codm, StudentSpouseRegistryRequestCommand command, CancellationToken cancellationToken) {

        var request = new GetIdentityInfoByNationalCodeRequest(codm, command.SpouseNationalCode, command.SpouseBirthDate.StringDateToInt().Value);
        var hoviat = await csisWsmService.GetIdentityInfoByNationalCode(request, cancellationToken);

        var prcRequest = new StudentDependentRegistryPrcRequest {
            Relation = DependentRelation.Spouse,
            Codm = codm,
            NationalCode = command.SpouseNationalCode,
            FirstName = hoviat.Name,
            LastName = hoviat.Family,
            FatherName = hoviat.FatherName,
            MotherName = hoviat.SpecialFeild,
            BirthDate = hoviat.BirthDate.StringDateToInt(),
            Gender = (Gender) int.Parse(hoviat.Gender),
            Religion = command.Religion,
            Citizenship = Citizenship.Iranian,
            IsSadat = hoviat.Name.StartsWith("سید") || hoviat.Name.EndsWith("سادات"),
            BirthCertSeri = hoviat.ShenasnameSeri,
            BirthCertSerial = int.Parse(hoviat.ShenasnameSerial),
            BirthCertNumber = hoviat.ShenasnameNo,
            BirthCertIssuePlace = hoviat.ShenasnameIssuePlace,
            SingleStatus = null,
            IsMarried = true,
            MarriageDate = command.MarriageDate.StringDateToInt(),
            DeathDate = null,
            IsDead = hoviat.DeathStatus != "0",
            YektaCode = null,
            Nationality = (int) Nationality.Iranian,
            PassportNumber = null,
            ResidenceExpireDate = null
        };
        prcRequest.DeathDate = hoviat.DeathDate.StringDateToInt();

        await Task.CompletedTask;
        return prcRequest;
    }
}


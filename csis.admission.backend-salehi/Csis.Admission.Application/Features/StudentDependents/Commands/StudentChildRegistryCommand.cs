using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using static Csis.Admission.Application.Common.Models.ValidateParentChildRelationshipRequest;

namespace Csis.Admission.Application.Features.StudentDependents.Commands;

/// <inheritdoc/>
public record StudentChildRegistryCommand(string NationalCode, string BirthDate, DependentChildRelation Relation, int? Codm = null) : IRequest<long>;

internal sealed class StudentChildRegistryCommandHandler(ICsisWsmService csisWsmService, IStudentRepository studentRepo,ICurrentUserService currentUser, 
    IStudentDependentRepository dependentRepo, IRepository<StudentSummary> studentSummaryRepo, IRepository<DependentSummary, long> dependentSummaryRepo)
    : IRequestHandler<StudentChildRegistryCommand, long>
{
    public async Task<long> Handle(StudentChildRegistryCommand command, CancellationToken cancellationToken) {
        await currentUser.SetCodm(command);
        var student = await studentRepo.GetStudentInfoByCodm(command.Codm.Value);
        if ( student.Citizenship != Citizenship.Iranian ) {
            throw new CommandValidationException("در سامانه سخا امکان ثبت اعضای خانواده برای طلاب غیر ایرانی وجود ندارد.");
        }

        // تکراری نباشد
        var exist = await dependentSummaryRepo.GetOneAsync(x => x.NationalCode == command.NationalCode, false, cancellationToken);
        if ( exist != null && exist.Codm != student.Codm ) { throw new CommandValidationException("این کد ملی به عنوان تکفل برای طلبه دیگری ثبت شده است."); }
        if ( exist != null ) { throw new CommandValidationException("این کد ملی پیش از این به عنوان تکفل برای طلبه ثبت شده است."); }

        // بررسی کد ملی و نسبت در ثبت احوال + ثبت و ذخیره
        var relation = await SabteAhvalRelation(student, command, cancellationToken);
        var request = SabteAhvalHoviat(student, command, relation);
        var result = await dependentRepo.Create(request);
        return result.Id;
    }

    private async Task<GetIdentityInfoByNationalCodeResponse> SabteAhvalRelation(StudentInfoDto student, StudentChildRegistryCommand command,
        CancellationToken cancellationToken) {

        var identityRequest= new GetIdentityInfoByNationalCodeRequest(student.Codm, command.NationalCode, command.BirthDate.StringDateToInt().Value);
        var identityInfo=await csisWsmService.GetIdentityInfoByNationalCode(identityRequest, cancellationToken);
        if ( string.IsNullOrEmpty(identityInfo.Nin) ) { 
            throw new CommandValidationException("کد ملی یا تاریخ تولد وارد شده در ثبت احوال یافت نشد.");
        }

        var request = new ValidateParentChildRelationshipRequest(student.NationalCode, student.BirthDate,
            command.NationalCode, command.BirthDate, student.Gender == Gender.Female ? RelationTypeEnum.MotherChild : RelationTypeEnum.FatherChild);

        var result = await csisWsmService.ValidateParentChildRelationship(request, cancellationToken);

        if ( result.GetResult() != ValidateParentChildRelationshipResponse.Result.ValidRelation ) {
            throw new CommandValidationException("اطلاعات نسبت فرزندی در ثبت احوال ثبت نشده است.");
        }

        return result.ChildHoviatFull;
    }

    private StudentDependentRegistryPrcRequest SabteAhvalHoviat(StudentInfoDto student, StudentChildRegistryCommand command, 
        GetIdentityInfoByNationalCodeResponse hoviat) {
        var prcRequest = new StudentDependentRegistryPrcRequest {
            Relation = (DependentRelation) command.Relation,
            Codm = student.Codm,
            NationalCode = command.NationalCode,
            FirstName = hoviat.Name,
            LastName = hoviat.Family,
            FatherName = hoviat.FatherName,
            MotherName = hoviat.SpecialFeild,
            BirthDate = hoviat.BirthDate.StringDateToInt(),
            Gender = (Gender) int.Parse(hoviat.Gender),
            Religion = student.Religion,
            Citizenship = Citizenship.Iranian,
            IsSadat = student.IsSadat.GetValueOrDefault(),
            BirthCertSeri = hoviat.ShenasnameSeri,
            BirthCertSerial = int.Parse(hoviat.ShenasnameSerial),
            BirthCertNumber = hoviat.ShenasnameNo,
            BirthCertIssuePlace = hoviat.ShenasnameIssuePlace,
            SingleStatus = SingleStatus.Single,
            IsMarried = false,
            MarriageDate = null,
            DeathDate = null,
            IsDead = hoviat.DeathStatus != "0",
            YektaCode = null,
            Nationality = (int) Nationality.Iranian,
            PassportNumber = null,
            ResidenceExpireDate = null
        };
        prcRequest.DeathDate = hoviat.DeathDate.StringDateToInt();
        return prcRequest;
    }
}

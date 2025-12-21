using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.ValidateSpousalRelationship;
using static Csis.Admission.Application.Common.Models.ValidateSpousalRelationshipResponse;
using static Csis.Admission.Application.Common.Models.ValidateSpousalRelationship.ValidateSpousalRelationshipRequest;

namespace Csis.Admission.Application.Features.Marriages.Commands;

/// <summary>ثبت ازدواج طلبه خواهر</summary>
public sealed record UpdateStudentSisterMarriageCommand : IRequest
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public string MarriageDate { get; init; }

    /// <inheritdoc/>
    public string SpouseNationalCode { get; init; }

    /// <inheritdoc/>
    public string SpouseBirthDate { get; init; }
}

internal sealed class UpdateStudentSisterMarriageCommandHandler(
    ICsisWsmService csisWsmService,
    IStudentRepository studentRepository,
    IStudentDependentRepository studentDependentRepository,
    IRepository<DependentSummary, long> repository)
    : IRequestHandler<UpdateStudentSisterMarriageCommand>
{
    public async Task Handle(UpdateStudentSisterMarriageCommand command, CancellationToken cancellationToken) {

        var student = await studentRepository.GetByCodm(command.Codm)
            ?? throw new CommandValidationException("کد مرکز خدمات نامعتبر می باشد.");

        if ( student.Gender != Gender.Female ) {

            //استعلام از ثبت احوال
            await ValidateSpousalRelationship(command, student, cancellationToken);

            var request = new SisterStudentMarriagePrcRequest {
                Codm = student.Codm,
                MarriageDate = command.MarriageDate.StringDateToInt().Value
            };

            await studentDependentRepository.CreateSisterStudentMarriageAsync(request);
        }
    }

    private async Task ValidateSpousalRelationship(UpdateStudentSisterMarriageCommand command, StudentDto student, CancellationToken cancellationToken) {
        var request = new ValidateSpousalRelationshipRequest(
            command.Codm,
            student.NationalCode,
            student.BirthDate.StringDateToInt(),
            command.SpouseNationalCode,
            command.SpouseBirthDate,
            command.MarriageDate,
            RelationTypeEnum.Marriage);

        var response = await csisWsmService.ValidateSpousalRelationship(request, cancellationToken);
        if ( response is Result.InvalidNationalCode ) {
            throw new CommandValidationException("شماره ملی همسر نامعتبر می باشد.");
        }

        if ( response is not Result.ValidRelation ) {
            throw new CommandValidationException("اطلاعات ازدواج در ثبت احوال ثبت نشده است.");
        }
    }
}

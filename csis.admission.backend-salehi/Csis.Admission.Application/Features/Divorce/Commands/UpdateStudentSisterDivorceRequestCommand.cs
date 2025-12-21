using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.ValidateSpousalRelationship;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Divorce.Commands;

/// <inheritdoc/>
public sealed record UpdateStudentSisterDivorceRequestCommand : IRequest
{
    /// <summary>
    /// کد مرکز طلبه ی خواهر
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// تاریخ طلاق طلبه خواهر
    /// </summary>
    public string DivorceDate { get; init; }


    /// <summary>
    /// کد ملی همسر طلبه ی خواهر برای استعلام از ثبت احوال
    /// </summary>
    public string SpouseNationalCode { get; init; }

    /// <summary>
    /// تاریخ تولد همسر طلبه ی خواهر برای استعلام از ثبت احوال
    /// </summary>
    public string SpouseBirthDate { get; init; }
}

internal sealed class UpdateStudentSisterDivorceRequestCommandHandler : IRequestHandler<UpdateStudentSisterDivorceRequestCommand>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICsisWsmService _csisWsmService;
    private readonly IRequestService _requestService;

    public UpdateStudentSisterDivorceRequestCommandHandler(
        IStudentRepository studentRepository,
        ICsisWsmService csisWsmService,
        IStudentDependentRepository studentDependentRepository,
        IRepository<DependentSummary, long> studentDependentRepo,
        IRepository<Student> studentRepo,
        IRequestService requestService) {
        _studentRepository = studentRepository;
        _csisWsmService = csisWsmService;
        _requestService = requestService;
    }

    public async Task Handle(UpdateStudentSisterDivorceRequestCommand request, CancellationToken cancellationToken) {


        var student = await _studentRepository.GetStudentInfoByCodm(request.Codm)
            ?? throw new CommandValidationException(" کد مرکز صحیح نیست ");


        if ( student.IsMarried == false ) {
            throw new CommandValidationException(" طلبه مجرد می باشند ");
        }


        if ( student.IsDead ) {
            throw new CommandValidationException(" طلبه مرحوم می باشد ");
        }

        if ( student.Gender == Gender.Male ) {
            throw new CommandValidationException(" امکان ثبت طلاق از این طریق، فقط برای طلاب خواهر امکان پذیر است");
        }

        if ( student.Citizenship != Citizenship.Iranian ) {
            throw new CommandValidationException(" این عملیات فقط برای طلاب ایرانی امکان پذیر است ");
        }

        var spousalRelationshipRequest = new ValidateSpousalRelationshipRequest(
            codm: request.Codm,
            nationalCode: student.NationalCode,
            birthDate: student.BirthDate.StringDateToInt(),
            nationalCodeSpouse: request.SpouseNationalCode,
            birthDateSpouse: request.SpouseBirthDate,
            eventDate: request.DivorceDate,
            relationType: ValidateSpousalRelationshipRequest.RelationTypeEnum.Divorce);

        // نتیجه استعلام طلاق همسر از ثبت احوال
        var response = await _csisWsmService.ValidateSpousalRelationship(spousalRelationshipRequest, cancellationToken);
        if ( response is not ValidateSpousalRelationshipResponse.Result.ValidRelation ) {
            throw new CommandValidationException("خطای استعلام طلاق");
        }
        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration);
        var requestId = await _requestService.Create(requestCommand, cancellationToken);
    }

}

using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Notification;
using System.Text;

namespace Csis.Admission.Application.Features.DependentEmployments.Commands;

/// <summary>
/// ارتباط داده ای - اشتغال تکفل
/// </summary>
public sealed record class EmploymentDependentDataImportCommand : BaseCommandDto<EmploymentDependentDataImportCommand, DependentEmployment>, IRequest
{
    /// <summary>کد مرکز </summary>
    public int Codm { get; init; }

    /// <summary>شناسه تکفل</summary>
    public long DependentId { get; init; }

    /// <summary>اشتغال دارد</summary>
    public bool? IsEmployee { get; init; }

    /// <summary>نام محل کار</summary>
    public string EmployeeName { get; init; }
}

internal sealed class EmploymentDependentDataImportCommandHandler : IRequestHandler<EmploymentDependentDataImportCommand>
{
    private readonly IStudentDependentRepository _studentDependentRepository;
    private readonly IRepository<DependentEmployment> _dependentEmploymentRepo;
    private readonly IRepository<DependentSummary, long> _studentDependentRepo;
    private readonly ICsisNotificationService _csisNotificationService;


    public EmploymentDependentDataImportCommandHandler(
        IRepository<DependentEmployment> dependentEmploymentRepo,
        IStudentRepository studentRepository,
        IRepository<DependentSummary, long> studentDependentRepo,
        IStudentDependentRepository studentDependentRepository,
        ICsisNotificationService csisNotificationService
        ) {
        _dependentEmploymentRepo = dependentEmploymentRepo;
        _studentDependentRepo = studentDependentRepo;
        _studentDependentRepository = studentDependentRepository;
        _csisNotificationService = csisNotificationService;
    }

    public async Task Handle(EmploymentDependentDataImportCommand request, CancellationToken cancellationToken) {


        var dependent = await _studentDependentRepo.GetOneAsync(x => x.Codm == request.Codm && x.Id == request.DependentId, cancellationToken: cancellationToken)
            ?? throw new CommandValidationException("تکفل نامعتبر می باشد.");

        var employment = await _dependentEmploymentRepo.GetOneAsTrackingAsync
            (x => x.Codm == request.Codm && x.DependentId == request.DependentId, cancellationToken: cancellationToken);

        //insert or update
        if ( employment is null ) {
            employment = request.ToEntity();
            await _dependentEmploymentRepo.InsertAsync(employment, cancellationToken: cancellationToken);

        } else {
            var updateEmployment = request.ToEntity(employment);
            await _dependentEmploymentRepo.UpdateAsync(updateEmployment, cancellationToken: cancellationToken);
        }

        //deactivate dependent
        if ( request.IsEmployee == true && dependent.IsActive ) {
            var procedureModel = new DeActiveDependentV4Model(request.Codm, request.DependentId, DependentDeActiveReasonEnum.Employment);
            await _studentDependentRepository.DeActiveCaseDependent(procedureModel);



            var message = new StringBuilder();
            message.Append("سلام علیکم ، طلبه گرامی اشتغال  ");
            message.Append($" {dependent.FirstName} ");
            message.Append($" {dependent.LastName} ");
            message.Append(" در ");
            message.Append($" {request.EmployeeName} ");
            message.Append("ثبت شد. مرکز خدمات حوزه های علمیه");

            await _csisNotificationService.SendMessageToStudent(new SendMessageToStudent(
                 message.ToString(),
                 [request.Codm],
                 [DeliveryChannelEnum.Sms]
            ), cancellationToken);
        }
    }
}

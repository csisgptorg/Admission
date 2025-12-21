
using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Notification;
using System.Text;

namespace Csis.Admission.Application.Features.Employments.Commands;

/// <summary>
/// ارتباطات داده ای - اشتغال
/// </summary>
public sealed record class EmployeeDataImportCommand :BaseCommandDto<EmployeeDataImportCommand, StudentEmployment>, IRequest
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// آیا شاغل است؟
    /// </summary>
    public bool IsEmployee { get; set; }

    /// <summary>
    /// نام محل کار
    /// </summary>
    public string EmployeeName { get; set; }
}

internal sealed class EmployeeDataImportCommandHandler : IRequestHandler<EmployeeDataImportCommand>
{
    private readonly IRepository<StudentEmployment> _repo;
    private readonly IMediator _mediator;
    private readonly IStudentRepository _studentRepository;
    private readonly ICsisNotificationService _csisNotificationService;
    public EmployeeDataImportCommandHandler(
        IRepository<StudentEmployment> repo,
        IMediator mediator,
        IStudentRepository studentRepository,
        ICsisNotificationService csisNotificationService) {
        _repo = repo;
        _mediator = mediator;
        _studentRepository = studentRepository;
        _csisNotificationService = csisNotificationService;
    }

    public async Task Handle(EmployeeDataImportCommand request, CancellationToken cancellationToken) {

        _ = await _studentRepository.GetByCodm(request.Codm)
            ?? throw new CommandValidationException("کد مرکز خدمات نامعتبر می باشد.");

        var employment = await _repo.GetOneAsTrackingAsync
            (x => x.Codm == request.Codm , cancellationToken: cancellationToken);

        //insert or update
        if ( employment is null ) {
            employment = request.ToEntity();
            await _repo.InsertAsync(employment, cancellationToken: cancellationToken);

        } else {
            var updateEmployment = request.ToEntity(employment);
            await _repo.UpdateAsync(updateEmployment, cancellationToken: cancellationToken);
        }


        var message = new StringBuilder();
        message.Append("سلام علیکم ، طلبه گرامی اشتغال شما در");
        message.Append($" {request.EmployeeName} ");
        message.Append("ثبت شد. مرکز خدمات حوزه های علمیه");

        await _csisNotificationService.SendMessageToStudent(new SendMessageToStudent(
             message.ToString(),
             [request.Codm],
             [DeliveryChannelEnum.Sms]
        ), cancellationToken);

    }
}

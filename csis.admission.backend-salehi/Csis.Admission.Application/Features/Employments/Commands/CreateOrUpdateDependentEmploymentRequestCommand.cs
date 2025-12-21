using Csis.Authorization.Services;
using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Employments.Commands;

/// <summary>À»  Ê »—Ê“—”«‰Ì Ê÷⁄Ì  «‘ €«·  ò›·</summary>
public record CreateOrUpdateDependentEmploymentRequestCommand : BaseCommandDto<CreateOrUpdateDependentEmploymentRequestCommand, DependentEmployment>, IRequest
{
    /// <summary>òœ „—ò“ Œœ„« </summary>
    public int Codm { get; set; }

    /// <summary>‘‰«”Â  ò›·</summary>
    public long DependentId { get; init; }

    /// <summary>¬Ì« ò«—„‰œ «” ø</summary>
    public bool IsEmployee { get; init; }

    /// <summary>‰«„ „Õ· ò«—</summary>
    public string EmployeeName { get; init; }

    /// <summary>¬œ—” „Õ· ò«—</summary>
    public string EmployeeAddress { get; init; }

    /// <summary>‘‰«”Â ›«Ì· ÅÌÊ” </summary>
    public Guid? FileId { get; init; }
}

internal sealed class CreateOrUpdateDependentEmploymentRequestCommandCommandHandler(
    IRequestService requestService,
    IRepository<DependentEmployment> repo,
    ICurrentUserService currentUser
    ) : IRequestHandler<CreateOrUpdateDependentEmploymentRequestCommand>
{
    public async Task Handle(CreateOrUpdateDependentEmploymentRequestCommand command, CancellationToken cancellationToken) {
        _ = await Common.Utilities.SetCodm(command, currentUser);
        var employment = await repo.GetOneAsTrackingAsync(x => x.Codm == command.Codm && x.DependentId == command.DependentId, false, cancellationToken);

        var flow = await GetFlowAndValidation(command, employment);
        var requestCommand = new CreateRequestCommand(command, flow);
        if ( command.FileId.HasValue ) {
            requestCommand.AddDocument(command.FileId.Value);
        }
        await requestService.Create(requestCommand, cancellationToken);
    }

    private async Task<RequestFlow> GetFlowAndValidation(CreateOrUpdateDependentEmploymentRequestCommand command, DependentEmployment employment) {
        if ( await currentUser.IsSenior() || employment == null || command.IsEmployee == employment.IsEmployee || (command.IsEmployee == true && employment.IsEmployee == false) || (command.IsEmployee && !command.FileId.HasValue) ) {
            return RequestFlow.DirectRegistration;
        }

        if ( await currentUser.IsEmployee() == true ) {
            return RequestFlow.EmployeeToSeniorEmployee;
        }

        if ( command.FileId == null ) {
            throw new CommandValidationException("»«—ê–«—Ì „œ—ò Å«Ì«‰ «‘ €«· œ— „Õ· ò«— ﬁ»·Ì «·“«„Ì „Ìù»«‘œ.");
        }

        return RequestFlow.StudentToEmployeeToSeniorEmployee;
    }
}

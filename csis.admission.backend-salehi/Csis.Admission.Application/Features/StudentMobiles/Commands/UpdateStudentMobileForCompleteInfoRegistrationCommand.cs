using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Models;
using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.StudentMobiles.Commands;

/// <inheritdoc/>
public sealed record UpdateStudentMobileForCompleteInfoRegistrationCommand(Guid Token, int Codm) : IRequest<long>;

//TODO نیازمند بازبینی
internal sealed class UpdateStudentMobileForCompleteInfoRegistrationCommandHandler : IRequestHandler<UpdateStudentMobileForCompleteInfoRegistrationCommand, long>
{
    private readonly ICsisWsmService _wsmService;
    private readonly IStudentMobileRepository _repo;
    private readonly IRequestService _requestService;
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    private readonly IRepository<AdmissionCaseUser, Guid> _admissionCaseUser;

    public UpdateStudentMobileForCompleteInfoRegistrationCommandHandler(IStudentMobileRepository repo, ICsisWsmService wsmService, IRequestService requestService,
        ICsisAuthenticatedUserService authenticatedUserService, IRepository<AdmissionCaseUser, Guid> admissionCaseUser) {
        _repo = repo;
        _wsmService = wsmService;
        _requestService = requestService;
        _authenticatedUserService = authenticatedUserService;
        _admissionCaseUser = admissionCaseUser;
    }

    //TODO پیچیدگی زیادی داریم بررسی و بهیود لازم است
    public async Task<long> Handle(UpdateStudentMobileForCompleteInfoRegistrationCommand command, CancellationToken cancellationToken) {

        var admissionCaseUser = await _admissionCaseUser.GetByIdAsync(command.Token, cancellationToken: cancellationToken)
            ?? throw new CommandValidationException("توکن نامعتبر است.");

        var updateCommand = new UpdateStudentMobileRepoCommand(command.Codm, admissionCaseUser.Mobile);

        var result = await _repo.Update(updateCommand);

        return result.Id;
    }

    private async Task<long> CreateRequest(UpdateStudentMobileRepoCommand command, CancellationToken cancellationToken) {
        var request = new CreateRequestCommand(command, RequestFlow.DirectRegistration);
        var requestId = await _requestService.Create(request, cancellationToken);
        return requestId;
    }
}

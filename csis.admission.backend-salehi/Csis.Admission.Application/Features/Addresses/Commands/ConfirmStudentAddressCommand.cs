using Csis.Utilities;
using Csis.Authorization.Services;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Addresses.Commands;

/// <inheritdoc/>
public sealed record ConfirmStudentAddressCommand :IRequest{}

internal sealed class ConfirmStudentAddressCommandHandler : IRequestHandler<ConfirmStudentAddressCommand>
{
    private readonly IRepository<Address> _repo;
    private readonly ICsisAuthenticatedUserService _authenticatedUser;
    public ConfirmStudentAddressCommandHandler(IRepository<Address> repo, ICsisAuthenticatedUserService authenticatedUser) {
        _repo = repo;
        _authenticatedUser = authenticatedUser;
    }

    public async Task Handle(ConfirmStudentAddressCommand request, CancellationToken cancellationToken) {
        var codm = int.Parse(await _authenticatedUser.GetStudentCodmAsync());
        var address = await _repo.GetOneAsTrackingAsync(x=>x.Codm==codm, cancellationToken:cancellationToken)
            ?? throw new RecordNotFoundException<Address>(codm);

        address.ConfirmDate= PersianDateTime.Now.ToString().StringDateToInt();
        await _repo.UpdateAsync(address, cancellationToken:cancellationToken);
    }
}

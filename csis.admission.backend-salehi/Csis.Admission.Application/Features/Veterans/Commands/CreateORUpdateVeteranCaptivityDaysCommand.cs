namespace Csis.Admission.Application.Features.Veterans.Commands;

/// <summary>
/// ویرایش روز آزادگی ایثارگری
/// </summary>
/// <param name="Codm">کد مرکز خدمات</param>
/// <param name="CaptivityDays">روز آزادگی</param>
public sealed record class CreateORUpdateVeteranCaptivityDaysCommand(int Codm, int CaptivityDays) : IRequest;

internal sealed class CreateORUpdateVeteranCaptivityDaysCommandHandler : IRequestHandler<CreateORUpdateVeteranCaptivityDaysCommand>
{
    private readonly IRepository<Veteran> _repo;
    public CreateORUpdateVeteranCaptivityDaysCommandHandler(IRepository<Veteran> repo) {
        _repo = repo;
    }

    public async Task Handle(CreateORUpdateVeteranCaptivityDaysCommand request, CancellationToken cancellationToken) {
        var veteran = await _repo.GetOneAsTrackingAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        if ( veteran is null ) {
            veteran = new Veteran { Codm = request.Codm, CaptivityDays = request.CaptivityDays };
            await _repo.InsertAsync(veteran, cancellationToken: cancellationToken);
        } else {
            veteran.CaptivityDays = request.CaptivityDays;
            await _repo.UpdateAsync(veteran, cancellationToken: cancellationToken);
        }
    }
}


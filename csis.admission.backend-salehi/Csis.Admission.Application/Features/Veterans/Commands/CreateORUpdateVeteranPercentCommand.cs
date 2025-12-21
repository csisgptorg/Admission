namespace Csis.Admission.Application.Features.Veterans.Commands;

/// <summary>ویرایش درصد جانبازی در ایثارگری</summary>
/// <param name="Codm">کد مرکز خدمات</param>
/// <param name="Percent">درصد جانبازی</param>
public sealed record class CreateORUpdateVeteranPercentCommand(int Codm,short Percent) : IRequest;

internal sealed class CreateORUpdateVeteranPercentCommandHandler : IRequestHandler<CreateORUpdateVeteranPercentCommand>
{
    private readonly IRepository<Veteran> _repo;
    public CreateORUpdateVeteranPercentCommandHandler(IRepository<Veteran>repo) {
        _repo=repo;
    }

    public async Task Handle(CreateORUpdateVeteranPercentCommand request, CancellationToken cancellationToken) {
        var veteran = await _repo.GetOneAsTrackingAsync(x => x.Codm == request.Codm);
        if ( veteran is null ) {
            veteran = new Veteran { Codm = request.Codm, CaptivityDays = request.Percent };
            await _repo.InsertAsync(veteran, cancellationToken: cancellationToken);
        } else {
            veteran.VeteranPercent = request.Percent;
            await _repo.UpdateAsync(veteran, cancellationToken: cancellationToken);
        }
    }
}


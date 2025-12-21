namespace Csis.Admission.Application.Features.Veterans.Commands;

/// <summary>
/// ویرایش نسبت با شهید ایثارگری
/// </summary>
/// <param name="Codm">کد مرکز خدمات</param>
/// <param name="RelationWithMartyr">نسبت با شهید</param>
public sealed record class CreateORUpdateVeteranRelationWithMartyrCommand(int Codm, DependentRelation RelationWithMartyr) : IRequest;

internal sealed class CreateORUpdateVeteranRelationWithMartyrHandler : IRequestHandler<CreateORUpdateVeteranRelationWithMartyrCommand>
{
    private readonly IRepository<Veteran> _repo;
    public CreateORUpdateVeteranRelationWithMartyrHandler(IRepository<Veteran>repo) {
        _repo=repo;
    }

    public async Task Handle(CreateORUpdateVeteranRelationWithMartyrCommand request, CancellationToken cancellationToken) {
        var veteran = await _repo.GetOneAsTrackingAsync(x => x.Codm == request.Codm);
        if ( veteran is null ) {
            veteran = new Veteran { Codm = request.Codm, RelationWithMartyr = request.RelationWithMartyr };
            await _repo.InsertAsync(veteran, cancellationToken: cancellationToken);
        } else {
            veteran.RelationWithMartyr = request.RelationWithMartyr;
            await _repo.UpdateAsync(veteran, cancellationToken: cancellationToken);
        }
    }
}


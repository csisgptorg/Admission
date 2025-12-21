namespace Csis.Admission.Application.Features.Veterans.Commands;

/// <summary>
/// حذف اطلاعات ایثارگری
/// </summary>
/// <param name="Codm">کد مرکز خدمات</param>
/// <param name="Id">شناسه ایثارگری</param>
public sealed record DeleteVeteranCommand(int Codm, int Id) : IRequest<int>;

internal sealed class DeleteVeteranCommandHandler(
    IRepository<Veteran> veteranRepository,
    ILogger<DeleteVeteranCommandHandler> logger)
    : IRequestHandler<DeleteVeteranCommand, int>
{
    public async Task<int> Handle(DeleteVeteranCommand request, CancellationToken cancellationToken) {
        // طی صحبت با سید , در عملیات حذف , همه فیلدها به جز کد مرکز خدمات Null میشوند
        var veteran = await veteranRepository.GetOneAsTrackingAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        veteran.CaptivityDays = null;
        veteran.ExileDays = null;
        veteran.HaramDefenceDays = null;
        veteran.HolyDefenseDays = null;
        veteran.JailDays = null;
        veteran.MartyrType = null;
        veteran.RelationWithMartyr = null;
        veteran.VeteranPercent = null;

        await veteranRepository.UpdateAsync(veteran, cancellationToken: cancellationToken);
        return request.Id;
    }
}

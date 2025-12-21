using Csis.Admission.Application.Common.Interfaces.Repositories.ImamJamaat;
using Csis.Admission.Application.Features.ImamJamaat.Dtos.Commands;

namespace Csis.Admission.Application.Features.ImamJamaat.Commands;

/// <summary>  
/// به‌روزرسانی اطلاعات مسجد با جزئیات امام جماعت و فعالیت‌های مسجد  
/// </summary>  
public sealed record UpdateMosqueWithDetailsCommand(int MosqueId, MosqueCommandDto Mosque, ImamJamaatCommandDto ImamJamaat, MosqueActivityCommandDto MosqueActivity, MosqueAddressCommandDto? MosqueAddress, int? MosqueAddressId) : IRequest;

internal sealed class UpdateMosqueWithDetailsCommandHandler(IMapper mapper, ILogger<UpdateMosqueWithDetailsCommandHandler> logger, IMosqueRepository mosqueRepository, IRepository<Domain.Entities.ImamJamaat> imamjamaatRepository) : IRequestHandler<UpdateMosqueWithDetailsCommand>
{
    public async Task Handle(UpdateMosqueWithDetailsCommand request, CancellationToken cancellationToken) {
        var foundedMosque = await mosqueRepository.GetMosqueFullInfoAsync(request.MosqueId, cancellationToken);

        if ( foundedMosque.Imams.Any(x => x.CodM != request.ImamJamaat.CodM) || request.Mosque.PostalCode != foundedMosque.PostalCode ) {
            var duplicate = await imamjamaatRepository.GetAllAsync(
                x => x.CodM == request.ImamJamaat.CodM && x.Mosque.PostalCode == request.Mosque.PostalCode,
                navigation: x => x.Mosque,
                cancellationToken: cancellationToken);

            if ( duplicate.Count != 0 ) {
                throw new CommandValidationException($"مسجد با کد پستی '{request.Mosque.PostalCode}' و کد مرکز '{request.ImamJamaat.CodM}' قبلاً ثبت شده است.");
            }
        }

        mapper.Map(request.Mosque, foundedMosque);
        mapper.Map(request.ImamJamaat, foundedMosque.Imams.First());
        mapper.Map(request.MosqueActivity, foundedMosque.MosqueActivity);
        if ( request.MosqueAddressId.HasValue ) {
            foundedMosque.MosqueAddressId = request.MosqueAddressId;
        } else {
            mapper.Map(request.MosqueAddress, foundedMosque.MosqueAddress);
        }

        await mosqueRepository.UpdateAsync(foundedMosque, autoSave: true, cancellationToken);

        logger.LogInformation("✅ اطلاعات مسجد با ID {MosqueId} با موفقیت به‌روزرسانی شد.", foundedMosque.Id);
    }
}

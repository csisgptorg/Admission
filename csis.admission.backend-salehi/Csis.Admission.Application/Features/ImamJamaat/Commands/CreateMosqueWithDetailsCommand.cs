using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Features.ImamJamaat.Dtos.Commands;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Authorization.Services;
using Microsoft.AspNetCore.Http;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Csis.Admission.Application.Features.ImamJamaat.Commands;

/// <summary>
/// دستور ایجاد مسجد با جزئیات
/// </summary>
public sealed record CreateMosqueWithDetailsCommand : IRequest<int>
{
    /// <summary>
    /// مدل مسجد
    /// </summary>
    public MosqueCommandDto Mosque { get; init; }

    /// <summary>
    /// امام جماعت مسجد
    /// </summary>
    public ImamJamaatCommandDto ImamJamaat { get; init; }

    /// <summary>
    /// فعالیت مسجد
    /// </summary>
    public MosqueActivityCommandDto MosqueActivity { get; init; }

    /// <summary>
    /// آدرس مسجد
    /// </summary>
    public MosqueAddressCommandDto? MosqueAddress { get; init; }

    /// <summary>
    /// شناسه آدرس مسجد
    /// </summary>
    public int? MosqueAddressId { get; init; }
}

internal sealed class CreateMosqueWithDetailsCommandHandler(
    ILogger<CreateMosqueWithDetailsCommandHandler> logger, IRepository<Mosque> mosqueRepository, IStudentRepository studentRepository, IMediator mediator
    , ICsisAuthenticatedUserService authenticatedUser, IHttpContextAccessor context
) : IRequestHandler<CreateMosqueWithDetailsCommand, int>
{
    public async Task<int> Handle(CreateMosqueWithDetailsCommand request, CancellationToken cancellationToken) {
        var imamJamaatInfo = await studentRepository.GetStudentInfoByCodm(request.ImamJamaat.CodM);

        await mediator.Send(new ImamJamaatCanRegisterQuery(request.ImamJamaat.CodM), cancellationToken);

        var mosque = request.Mosque.ToEntity();
        mosque.Codm = imamJamaatInfo.Codm;
        var imamJamaat = request.ImamJamaat.ToEntity();
        imamJamaat.SetFullName(imamJamaatInfo.FirstName, imamJamaatInfo.LastName);
        var mosqueActivity = request.MosqueActivity.ToEntity();
        var mosqueAddress = new Address();
        if ( request.MosqueAddress != null ) {
            mosqueAddress = request.MosqueAddress.ToEntity();
            mosqueAddress.Codm = imamJamaatInfo.Codm;
        }

        if ( await mosqueRepository.ExistsAsync(x => x.PostalCode == mosque.PostalCode && x.PostalCode != null && x.Imams.Any(x => x.CodM == imamJamaat.CodM), cancellationToken: cancellationToken) ) {
            throw new CommandValidationException($"مسجد با کد پستی '{mosque.PostalCode}' و کد مرکز '{imamJamaat.CodM}' قبلاً ثبت شده است.");
        }

        mosque.MosqueActivity = mosqueActivity;
        if ( request.MosqueAddressId.HasValue ) {
            mosque.MosqueAddressId = request.MosqueAddressId.Value;
        } else {
            mosque.MosqueAddress = mosqueAddress;
        }
        mosque.Imams = [imamJamaat];
        imamJamaat.Mosque = mosque;

        await mosqueRepository.InsertAsync(mosque, autoSave: true, cancellationToken: cancellationToken);

        logger.LogInformation("مسجد با ID {MosqueId} و امام جماعت با ID {ImamJamaatId} با موفقیت ثبت شدند.",
            mosque.Id, imamJamaat.Id);

        return mosque.Id;
    }
}

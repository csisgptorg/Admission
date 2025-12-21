using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;
using Csis.Admission.Application.Features.ImamJamaat.Dtos.Commands;
using Csis.Admission.Application.Features.ImamJamaat.Queries;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Domain.Entities;
using Csis.Authorization.Services;
using Microsoft.AspNetCore.Http;

namespace Csis.Admission.Application.Features.ImamJamaat.Commands;

/// <summary> دستور ایجاد مسجد با جزئیات </summary>
public sealed record CreateMosqueWithDetailsStudentCommand : IRequest<int>
{
    /// <summary> مدل مسجد </summary>
    public MosqueCommandDto Mosque { get; init; }

    /// <summary> امام جماعت مسجد </summary>
    public ImamJamaatStudentCommandDto ImamJamaat { get; init; }

    /// <summary> فعالیت مسجد </summary>
    public MosqueActivityCommandDto MosqueActivity { get; init; }

    /// <summary> آدرس مسجد </summary>
    public MosqueAddressCommandDto MosqueAddress { get; init; }

    /// <summary> شناسه آدرس مسجد </summary>
    public int? MosqueAddressId { get; init; }
}

internal sealed class CreateMosqueWithDetailsStudentCommandHandler(
  ILogger<CreateMosqueWithDetailsStudentCommandHandler> logger, IRepository<Mosque> mosqueRepository, IStudentRepository studentRepository, IMediator mediator, ICsisAuthenticatedUserService authenticatedUser, IHttpContextAccessor context) : IRequestHandler<CreateMosqueWithDetailsStudentCommand, int>
{
    public async Task<int> Handle(CreateMosqueWithDetailsStudentCommand request, CancellationToken cancellationToken) {
        var imamJamaatInfo = await studentRepository.GetStudentInfoByCodm(request.ImamJamaat.CodM);

        await mediator.Send(new ImamJamaatCanRegisterQuery(request.ImamJamaat.CodM), cancellationToken);

        if ( !await HasPreachHistory(imamJamaatInfo.Codm) ) {
            throw new CommandValidationException("امکان ثبت مسجد برای شما وجود ندارد. لطفا با پشتیبانی تماس بگیرید.");
        }

        var mosque = request.Mosque.ToEntity();
        mosque.Codm = imamJamaatInfo.Codm;
        var imamJamaat = request.ImamJamaat.ToEntity();
        imamJamaat.SetFullName(imamJamaatInfo?.FirstName, imamJamaatInfo?.LastName);
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

        logger.LogInformation(
            "✅ مسجد با ID {MosqueId} و امام جماعت با ID {ImamJamaatId} با موفقیت ثبت شدند.",
            mosque.Id, imamJamaat.Id
        );

        return mosque.Id;
    }

    private async Task<bool> HasPreachHistory(int Codm) {
        var result = await mediator.Send(new GetStudentPossibilityToCreateMosqueQuery(Codm));
        return result;
    }
}

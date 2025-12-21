using Csis.Admission.Application.Features.ImamJamaat.Commands;

namespace Csis.Admission.Application.Features.ImamJamaat.Queries;

/// <summary>
/// (امکان ثبت مسجد توسط طلبه (اطلاعات تلیغ
/// </summary>
/// <param name="Codm"></param>
public sealed record GetStudentPossibilityToCreateMosqueQuery(int Codm) : IRequest<bool>;
internal sealed class GetStudentPossibilityToCreateMosqueQueryHandler(
    ILogger<GetStudentPossibilityToCreateMosqueQueryHandler> logger,
    IRepository<Preach> preachRepository, IMediator mediator)
    : IRequestHandler<GetStudentPossibilityToCreateMosqueQuery, bool>
{
    public async Task<bool> Handle(GetStudentPossibilityToCreateMosqueQuery request, CancellationToken cancellationToken) {
        var result = false;

        var founded = await preachRepository.GetAllAsync(x => x.Codm == request.Codm, cancellationToken: cancellationToken);
        if ( founded.Count > 0 ) {
            result = founded.Any(x => x.Kind == PreachKind.ImamJamaat || x.Kind == PreachKind.TarhHejratBolandModat || x.Kind == PreachKind.RohaniMostaghar);
        }

        var canRegister = await mediator.Send(new ImamJamaatCanRegisterQuery(request.Codm), cancellationToken);

        return result && canRegister;
    }
}


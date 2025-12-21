using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.NonIranianStudent.Commands;


/// <inheritdoc/>
public sealed record UpdateNonIranianWifeDivorceRequestCommand : IRequest
{
    public int Codm { get; set; }
    public long? DependentId { get; init; }
    public string DivorceDate { get; init; }
}

internal sealed class UpdateNonIranianWifeDivorceRequestCommandHandler(
    IStudentRepository studentRepository,
    ICurrentUserService currentUserService,
    IRepository<DependentSummary, long> studentDependentRepo,
    IRequestService requestService)
    : IRequestHandler<UpdateNonIranianWifeDivorceRequestCommand>
{
    public async Task Handle(UpdateNonIranianWifeDivorceRequestCommand request, CancellationToken cancellationToken) {
        var student = await studentRepository.GetStudentInfoByCodm(request.Codm)
                      ?? throw new CommandValidationException(" کد مرکز صحیح نیست ");


        var dependent = await studentDependentRepo.GetByIdAsync(request.DependentId.Value, cancellationToken: cancellationToken)
                        ?? throw new CommandValidationException(" آیدی همسر صحیح نیست ");

        if ( student.IsMarried ) {
            if ( student.IsDead && !(await currentUserService.PersonnelId()).HasValue ) {
                throw new CommandValidationException(" طلبه مرحوم می باشد ");
            }

            if ( dependent.Relation != DependentRelation.Spouse ) {
                throw new CommandValidationException(" فرد انتخاب شده همسر نمی باشد ");
            }

            if ( !dependent.IsMarried || dependent.IsDead ) {
                throw new CommandValidationException("فرد انتخاب شده همسر نمی باشد یا در قید حیات نیست.");
            }

            var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration);
            await requestService.Create(requestCommand, cancellationToken);

        } else {
            throw new CommandValidationException(" طلبه مجرد می باشد ");
        }
    }
}

using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.NonIranianStudent.Commands;

/// <summary>
/// ثبت درخواست تغییر وضعیت تاهل تکفل غیر ایرانی
/// </summary>
public sealed record UpdateNonIranianDependentMarriageRequestCommand : IRequest
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public string MarriageDate { get; init; }

    /// <inheritdoc/>
    public long? DependentId { get; init; }
}

internal sealed class UpdateNonIranianDependentMarriageCommandHandler(
    IStudentRepository studentRepository,
    IRepository<DependentSummary, long> studentDependentRepo,
    IRequestService requestService)
    : IRequestHandler<UpdateNonIranianDependentMarriageRequestCommand>
{

    public async Task Handle(UpdateNonIranianDependentMarriageRequestCommand request,
        CancellationToken cancellationToken) {

        var student = await studentRepository.GetStudentInfoByCodm(request.Codm)
                      ?? throw new CommandValidationException(" کد مرکز صحیح نیست ");

        var dependent =
            await studentDependentRepo.GetByIdAsync(request.DependentId.Value, cancellationToken: cancellationToken)
            ?? throw new CommandValidationException(" آیدی تکفل صحیح نیست ");

        if ( student.Citizenship == Citizenship.NonIranian ) {

            if ( !student.IsMarried ) {
                throw new CommandValidationException(" طلبه مجرد می باشد ");
            }

            if ( dependent.IsMarried ) {
                throw new CommandValidationException("تکفل انتخاب شده متاهل می باشد ");
            }

            if ( dependent.IsDead ) {
                throw new CommandValidationException(" تکفل انتخاب شده مرحوم می باشد ");
            }

            if ( dependent.DivorceDate.HasValue ) {
                throw new CommandValidationException("پرونده تکفل انتخاب شده مسدود می باشد ");
            }

            var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration);
            await requestService.Create(requestCommand, cancellationToken);
        } else {
            throw new CommandValidationException(" این درخواست فقط برای غیر ایرانیان می باشد ");
        }

    }
}

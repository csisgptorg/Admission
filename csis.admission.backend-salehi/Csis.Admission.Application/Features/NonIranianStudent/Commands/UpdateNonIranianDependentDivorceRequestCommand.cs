using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.NonIranianStudent.Commands;

/// <summary>
/// ثبت درخواست تغییر وضعیت طلاق تکفل غیر ایرانی
/// </summary>
public sealed record UpdateNonIranianDependentDivorceRequestCommand : IRequest
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public string DivorceDate { get; init; }

    /// <inheritdoc/>
    public long? DependentId { get; init; }

    /// <summary>
    ///  مدرک طلاق‌
    /// </summary>
    public Guid FileId { get; init; }
}

internal sealed class UpdateNonIranianDependentDivorceCommandHandler(
    IStudentRepository studentRepository,
    ICurrentUserService currentUserService,
    IRepository<DependentSummary, long> studentDependentRepo,
    IRequestService requestService)
    : IRequestHandler<UpdateNonIranianDependentDivorceRequestCommand>
{

    public async Task Handle(UpdateNonIranianDependentDivorceRequestCommand request,
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

            if ( !dependent.IsMarried ) {
                throw new CommandValidationException("تکفل انتخاب شده متاهل نمی باشد ");
            }

            if ( dependent.DivorceDate.HasValue ) {
                throw new CommandValidationException("پرونده تکفل انتخاب شده مسدود می باشد ");
            }

            var requestFlow = (await currentUserService.PersonnelId()).HasValue ? RequestFlow.DirectRegistration : RequestFlow.StudentToEmployee;
            var dependentDivorceRequestCommand = new CreateRequestCommand(request, requestFlow);
            dependentDivorceRequestCommand.AddDocument(request.FileId);
            await requestService.Create(dependentDivorceRequestCommand, cancellationToken);

        } else {
            throw new CommandValidationException(" این درخواست فقط برای غیر ایرانیان می باشد ");
        }

    }
}

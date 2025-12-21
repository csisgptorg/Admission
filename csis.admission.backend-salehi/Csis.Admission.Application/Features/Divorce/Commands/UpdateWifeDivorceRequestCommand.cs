using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.ValidateSpousalRelationship;
using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Features.Divorce.Commands;


/// <inheritdoc/>
public sealed record UpdateWifeDivorceRequestCommand : IRequest
{
    /// <summary>
    /// کد مرکز خدمات
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// آیدی همسر
    /// </summary>
    public long? DependentId { get; init; }

    /// <summary>
    /// تاریخ طلاق
    /// </summary>
    public string DivorceDate { get; init; }

    /// <summary>
    /// شماره ملی همسر
    /// </summary>
    public string SpouseNationalCode { get; init; }

    /// <summary>
    /// تاریخ تولد همسر
    /// </summary>
    public string SpouseBirthDate { get; init; }
}

internal sealed class UpdateWifeDivorceRequestCommandHandler(
    IStudentRepository studentRepository,
    IRepository<DependentSummary, long> studentDependentRepo,
    IRequestService requestService)
    : IRequestHandler<UpdateWifeDivorceRequestCommand>
{
    public async Task Handle(UpdateWifeDivorceRequestCommand request, CancellationToken cancellationToken) {
        var student = await studentRepository.GetStudentInfoByCodm(request.Codm)
                      ?? throw new CommandValidationException(" کد مرکز صحیح نیست ");


        var dependent = await studentDependentRepo.GetByIdAsync(request.DependentId.Value, cancellationToken: cancellationToken)
                        ?? throw new CommandValidationException(" آیدی همسر صحیح نیست ");

        if ( student.IsMarried ) {

            if ( dependent.Relation != DependentRelation.Spouse ) {
                throw new CommandValidationException(" فرد انتخاب شده همسر نمی باشد ");
            }

            if ( dependent.NationalCode != request.SpouseNationalCode ) {
                throw new CommandValidationException(" شماره ملی همسر با اطلاعات آن مغایرت دارد ");
            }

            if ( !dependent.IsMarried || dependent.IsDead  ) {
                throw new CommandValidationException("فرد انتخاب شده همسر نمی باشد یا در قید حیات نیست.");
            }

            if ( student.Citizenship != Citizenship.Iranian ) {
                throw new CommandValidationException(" این عملیات فقط برای طلاب ایرانی امکان پذیر است ");
            }

            var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration);
            await requestService.Create(requestCommand, cancellationToken);
        } else {
            throw new CommandValidationException(" طلبه مجرد می باشد ");
        }
    }
}


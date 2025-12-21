using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.Students.Queries;
using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>
/// تمدید پرونده
/// </summary>
public sealed record StudentExtensionCaseCommand(int? Codm = null) : IRequest<ProcedureResultDto>;

internal sealed class StudentExtensionCaseCommandHandler(
    IStudentRepository repo,
    IMediator mediator,
    ICsisAuthenticatedUserService authenticatedUserService)
    : IRequestHandler<StudentExtensionCaseCommand, ProcedureResultDto>
{
    public async Task<ProcedureResultDto> Handle(StudentExtensionCaseCommand request, CancellationToken cancellationToken) {
        var isUserEmployee = await authenticatedUserService.IsEmployeeLoggedInAsync();

        StudentExtensionCaseCommandPrc command = null;
        switch ( isUserEmployee ) {

            case true when await CalcCanExtensionCase(request.Codm.Value):
                var userId = await authenticatedUserService.GetUserIdAsync();
                var personnelId = await authenticatedUserService.GetPersonnelIdAsync();
                command = StudentExtensionCaseFactory(request.Codm.Value, personnelId, userId);
                break;

            case false:
                var codm = int.Parse(await authenticatedUserService.GetStudentCodmAsync());
                command = StudentExtensionCaseFactory(codm);
                break;
        }

        var result = await repo.ExtensionCaseCommand(command);
        return result;
    }

    /// <summary>
    /// سازنده دستور تمدید پرونده
    /// </summary>
    /// <param name="codm"></param>
    /// <param name="personnelId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    private StudentExtensionCaseCommandPrc StudentExtensionCaseFactory(int codm, int? personnelId = null, int? userId = null)
        => new() {
            Codm = codm,
            PersonnelId = personnelId,
            UserId = userId ?? codm,
            DataSource = personnelId.HasValue ? DataSource.Employee : DataSource.Student,
            ApplicationId = 66,
            RequestId = codm
        };

    /// <summary>
    /// محاسبه زمان برای امکان تمدید پرونده
    /// </summary>
    /// <param name="codm"></param>
    /// <returns></returns>
    /// <exception cref="CommandValidationException"></exception>
    private async Task<bool> CalcCanExtensionCase(int codm) {
        var result = await mediator.Send(new GetStudentSummaryCaseByCodmQuery(codm));
        if ( result.IsBlock ) {
            throw new CommandValidationException("پرونده شما مسدود می باشد. جهت رفع مسدودی با پشتیبانی تماس بگیرید.");
        }
        // 00000300 معادل سه ماه به صورت عددی
        return DateTime.Now.ToPersianInteger() >= (result.CaseValidityDate.StringDateToInt() - 00000300) ? true : throw new CommandValidationException("تمدید پرونده تنها در سه ماه پایانی اعتبار پرونده امکان پذیر می باشد.");
    }

}

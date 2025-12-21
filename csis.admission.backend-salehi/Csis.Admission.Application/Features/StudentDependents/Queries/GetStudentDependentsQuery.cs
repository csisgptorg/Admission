using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.StudentDependents.Dtos;

namespace Csis.Admission.Application.Features.StudentDependents.Queries;

/// <summary>
/// دریافت لیست تکفل ها + خود طلبه برای استفاده در نرم افزار جهیزیه
/// </summary>
/// <param name="Codm"></param>
public sealed record GetStudentDependentsQuery(int Codm) : IRequest<StudentWithDependentsDto>;

internal sealed class GetStudentDependentsQueryHandler(
    IRepository<StudentSummary> studentSummaryRepo,
    IRepository<DependentSummary, long> studentDependentRepo)
    : IRequestHandler<GetStudentDependentsQuery, StudentWithDependentsDto>
{
    public async Task<StudentWithDependentsDto> Handle(GetStudentDependentsQuery request, CancellationToken cancellationToken) {
        var student = await studentSummaryRepo.GetOneAsync(x => x.Codm == request.Codm)
            ?? throw new CommandValidationException("کد مرکز صحیح نیست");

        var dependentList = await studentDependentRepo.GetAllAsync<StudentDependentDto>(x => x.Codm == request.Codm && x.Relation == DependentRelation.Child && !x.DivorceDate.HasValue && x.IsMarried && !x.IsDead, cancellationToken: cancellationToken);

        return new StudentWithDependentsDto {
            Student = new StudentDependentDto {
                Id = student.Id,
                Codm = student.Codm,
                FirstName = student.FirstName,
                LastName = student.LastName,
                MarriageDate = student.MarriageDate.HasValue ? student.MarriageDate.Value.IntDateToString() : null,
                IsMarried = student.IsMarried,
                IsDead = student.IsDead,
                IsActive = student.IsActive,
                DependentId = null
            },
            Dependents = [.. dependentList.Select(x=> new StudentDependentDto {
                Id = x.Id,
                Codm = x.Codm,
                DependentId = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                MarriageDate = x.MarriageDate,
                IsMarried = x.IsMarried,
                IsDead = x.IsDead,
                IsActive = x.IsActive
            })]
        };
    }
}

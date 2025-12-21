using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.CaseFilings.Commands;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Csis.Admission.Persistence.Repositories.Students;

internal sealed class StudentSummaryRepository(AppDbContext dbContext) : IStudentSummaryRepository
{
    public async Task<StudentECardDto> GetStudentElectronicCardByCodm(int codm) {
        return await dbContext.Set<StudentSummary>()
             .AsNoTracking()
             .Where(x => x.Codm == codm)
             .GroupJoin(dbContext.Set<TargetScore>(), x => x.Codm, y => y.Codm, (x, y) => new { x, y })
             .Select(joinedValue => new StudentECardDto {
                 NationalCode = joinedValue.x.NationalCode,
                 Codm = joinedValue.x.Codm,
                 FirstName = joinedValue.x.FirstName,
                 LastName = joinedValue.x.LastName,
                 CaseValidityDate = joinedValue.x.CaseValidityDate.Value.IntDateToString(),
                 Grade = joinedValue.x.Taraz.Value,
                 IsBlock = joinedValue.x.IsBlock,
                 IsPreacher = joinedValue.y.Select(score => score.IsPreacher != false).FirstOrDefault(),
                 IsResearcher = joinedValue.y.Select(score => score.IsResearcher != false).FirstOrDefault(),
                 IsTeacher = joinedValue.y.Select(score => score.IsTeacher != false).FirstOrDefault()
             })
             .FirstOrDefaultAsync();
    }
}

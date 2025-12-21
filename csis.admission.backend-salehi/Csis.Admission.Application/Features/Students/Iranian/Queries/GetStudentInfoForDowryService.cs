using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.Students.Dtos;
using NPOI.SS.Formula.Functions;

namespace Csis.Admission.Application.Features.Students.Iranian.Queries;

/// <summary>
/// دريافت اطلاعات طلبه براي جهيزيه
/// </summary>
/// <param name="Codms"></param>
public sealed record GetStudentInfoForDowryService(List<int?> Codms) : IRequest<List<StudentWithDependentInfoForDowryServiceDto>>;

internal sealed class GetStudentInfoForDowryServiceHandler(IRepository<StudentSummary> studentRepo, IRepository<DependentSummary, long> dependentRepository, IRepository<ShiaMinitory> shiaRepository, IRepository<StudentEmployment> studentEmploymentRepository, IRepository<TargetedScoreHistory> TargetScoreRepository, IRepository<DependentEmployment> dependentEmploymentRepository) : IRequestHandler<GetStudentInfoForDowryService, List<StudentWithDependentInfoForDowryServiceDto>>
{
    public async Task<List<StudentWithDependentInfoForDowryServiceDto>> Handle(GetStudentInfoForDowryService request, CancellationToken cancellationToken) {

        //TODO: VPoor رو باید سید درست کنه, فعلا دارین null میفرستیم
        var result = (
           studentSummaries: new List<StudentSummary>(), studentEmployments: new List<StudentEmployment>(), dependentSummaries: new List<DependentSummary>(),
           dependentEmployments: new List<DependentEmployment>(), shiaMinistries: new List<ShiaMinitory>(), VPoor: new List<TargetedScoreHistory>()
        );

        if ( request.Codms.Any() ) {
            result.studentSummaries = await studentRepo.GetAllAsync(x => request.Codms.Contains(x.Codm), cancellationToken: cancellationToken);
            result.studentEmployments = await studentEmploymentRepository.GetAllAsync(x => request.Codms.Contains(x.Codm), cancellationToken: cancellationToken);
            result.dependentSummaries = await dependentRepository.GetAllAsync(x => request.Codms.Contains(x.Codm) && x.Relation == DependentRelation.Child && !x.DivorceDate.HasValue && x.IsMarried && !x.IsDead, cancellationToken: cancellationToken);
            result.dependentEmployments = await dependentEmploymentRepository.GetAllAsync(x => result.dependentSummaries.Select(d => d.Id).Contains(x.DependentId), cancellationToken: cancellationToken);
            result.shiaMinistries = await shiaRepository.GetAllAsync(x => request.Codms.Contains(x.Codm), cancellationToken: cancellationToken);

            return [.. result.studentSummaries
    .Select(student => new StudentWithDependentInfoForDowryServiceDto(
        new InfoForDowryServiceDto {
            Codm = student.Codm,
            DependentId = null,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Decile = result.studentEmployments.FirstOrDefault(se => se.Codm == student.Codm)?.Decile,
            IsHeadOfHousehold = true,
            IsMarried = student.IsMarried,
            MarriageDate = student.MarriageDate.HasValue ? student.MarriageDate.Value.IntDateToString() : null,
            IsLivingInShiaMinorityArea = result.shiaMinistries.Any(sm => sm.Codm == student.Codm),
            IsLivingInPoorArea = null //result.VPoor.Any(vp => vp.Codm == student.Codm),
        },
        [.. result.dependentSummaries
            .Where(dependent => dependent.Codm == student.Codm)
            .Select(dependent => new InfoForDowryServiceDto {
                Codm = dependent.Codm,
                DependentId = dependent.Id,
                FirstName = dependent.FirstName,
                LastName = dependent.LastName,
                Decile = result.dependentEmployments.FirstOrDefault(de => de.DependentId == dependent.Id)?.Decile,
                IsHeadOfHousehold = false,
                IsMarried = dependent.IsMarried,
                MarriageDate = dependent.MarriageDate.HasValue ? dependent.MarriageDate.Value.IntDateToString() : null,
                IsLivingInShiaMinorityArea = result.shiaMinistries.Any(sm => sm.Codm == dependent.Codm),
                IsLivingInPoorArea = null
            })]
    ))];
        }
        throw new CommandValidationException("اطلاعات ورودي معتبر نمي باشد");
    }
}


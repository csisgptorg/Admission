using Csis.Admission.Application.Features.Students.Dtos;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <summary>
/// دریافت اطلاعات پرونده ای دانشجو بر اساس کد مرکز (Codm)
/// </summary>
/// <param name="Codm"></param>
public sealed record GetStudentSummaryCaseByCodmQuery(int Codm) : IRequest<StudentSummaryCaseDto>;

internal sealed class GetStudentSummaryCaseByCodmQueryHandler(IRepository<StudentSummary> studentRepo)
    : IRequestHandler<GetStudentSummaryCaseByCodmQuery, StudentSummaryCaseDto>
{
    public async Task<StudentSummaryCaseDto> Handle(GetStudentSummaryCaseByCodmQuery request, CancellationToken cancellationToken) {

        return await studentRepo.GetOneAsync<StudentSummaryCaseDto>(x => x.Codm == request.Codm, cancellationToken: cancellationToken)
            ?? throw new CommandValidationException("پرونده ای با این مشخصات یافت نشد.");
    }
}

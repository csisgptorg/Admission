using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <summary>œ—?«›  ”«»ﬁÂ „”ò‰</summary>
public sealed record GetStudentHouseHistoryByCodmQuery(int Codm) : IRequest<StudentHouseHistoryDto>;

internal sealed class GetStudentHouseHistoryByCodmQueryHandler : IRequestHandler<GetStudentHouseHistoryByCodmQuery, StudentHouseHistoryDto>
{
    private readonly IStudentRepository _studentRepo;
    public GetStudentHouseHistoryByCodmQueryHandler(IStudentRepository studentRepo) {
        _studentRepo = studentRepo;
    }

    public async Task<StudentHouseHistoryDto> Handle(GetStudentHouseHistoryByCodmQuery request, CancellationToken cancellationToken) {

        return await _studentRepo.GetHouseHistory(request.Codm)
            ?? throw new RecordNotFoundException<GetStudentHouseHistoryByCodmQuery>(request.Codm);
    }
}

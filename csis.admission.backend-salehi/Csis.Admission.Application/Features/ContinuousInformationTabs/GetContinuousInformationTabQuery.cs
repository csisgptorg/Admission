using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.ContinuousInformationTabs;

/// <summary>نمایش محتوای تب مشخصات مستمری</summary>
public sealed record class GetContinuousInformationTabQuery(int Codm, bool Employee = false) : IRequest<StudentPensionStatusDto>;

internal sealed class GetContinuousInformationTabQueryHandler : IRequestHandler<GetContinuousInformationTabQuery, StudentPensionStatusDto>
{
    private readonly IStudentRepository _studentRepository;

    public GetContinuousInformationTabQueryHandler(IStudentRepository studentRepository) {
        _studentRepository = studentRepository;
    }

    public async Task<StudentPensionStatusDto> Handle(GetContinuousInformationTabQuery request, CancellationToken cancellationToken) {
        if ( request.Employee ) {
            var x = await _studentRepository.GetByCodm(request.Codm)
                ?? throw new CommandValidationException("کد مرکز نامعتبر است.");
        }

        return await _studentRepository.GetContinuousInformationTabByCodm(request.Codm);
    }
}


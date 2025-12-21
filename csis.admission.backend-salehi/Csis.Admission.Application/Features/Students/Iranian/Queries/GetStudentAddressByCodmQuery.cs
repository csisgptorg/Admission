using Csis.Admission.Application.Features.Addresses.Dtos;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <summary>
/// Get student by codm
/// </summary>
/// <param name="Codm"></param>
public sealed record GetStudentAddressByCodmQuery(int Codm) : IRequest<AddressDto>;

internal sealed class GetStudentAddressByCodmQueryHandler(IRepository<Address> addressRepo) : IRequestHandler<GetStudentAddressByCodmQuery, AddressDto>
{
    public async Task<AddressDto> Handle(GetStudentAddressByCodmQuery request, CancellationToken cancellationToken) {
        var selfProjectCode = 1;
        var result= await addressRepo.GetOneAsync<AddressDto>(x => x.Codm == request.Codm && x.ProjectCode == selfProjectCode,
            cancellationToken: cancellationToken);
        return result ?? new AddressDto();
    }
}

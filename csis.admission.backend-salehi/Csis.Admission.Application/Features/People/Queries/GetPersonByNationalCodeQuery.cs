using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.People.Dtos;
using Csis.Shared.Kernel.Public.Extensions;

namespace Csis.Admission.Application.Features.People.Queries;

/// <summary>
/// دریافت موجودیت شخص با کد ملی
/// </summary>
public sealed record GetPersonByNationalCodeQuery(string NationalCode, int BirthDate) : IRequest<GetIdentityInfoByNationalCodeResponse>;

internal sealed class GetPersonByNationalCodeQueryHandler(
    IPersonRepository personRepo,
    ILogger<GetPersonByNationalCodeQueryHandler> logger)
    : IRequestHandler<GetPersonByNationalCodeQuery, GetIdentityInfoByNationalCodeResponse>
{
    public async Task<GetIdentityInfoByNationalCodeResponse> Handle(GetPersonByNationalCodeQuery request, CancellationToken cancellationToken) {

        if ( string.IsNullOrWhiteSpace(request.NationalCode) ) {
            throw new CommandValidationException("کد ملی وارد نشده است.");
        }

        var person = await personRepo.GetOneAsync<PersonDto>(
            x => x.NationalCode == request.NationalCode 
                 && x.BirthDate == request.BirthDate,
            cancellationToken: cancellationToken);

        return person != null ? MapToResponse(person) : null;
    }

    /// <summary>
    /// بر اساس مدل بازگشتی از ثبت احوال
    /// </summary>
    /// <param name="person"></param>
    /// <returns></returns>
    private static GetIdentityInfoByNationalCodeResponse MapToResponse(PersonDto person) =>
        new() {
            Id = person.Id,
            FatherName = person.FatherName,
            ShenasnameSeri = person.BirthCertSeri,
            ShenasnameNo = person.BirthCertNumber,
            ShenasnameSerial = person.BirthCertSerial,
            BirthDate = person.BirthDate,
            DeathStatus = person.DeathCause.HasValue ? ((int) person.DeathCause!.Value).ToString() : null,
            DeathDate = person.DeathDate,
            SpecialFeild = person.MotherName,
            ShenasnameIssuePlace = person.BirthCertIssuePlace,
            ShenasnameIssueDate = null,
            Gender = ((int) person.Gender).ToString(),
            Name = person.FirstName,
            Family = person.LastName,
            Nin = person.NationalCode,
            Birthplace = person.BirthCertIssueProvince,
            IsRegistered = true
        };
}

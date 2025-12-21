using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.People.Dtos;
using Csis.Shared.Kernel.Public.Extensions;
using Microsoft.OpenApi.Extensions;

namespace Csis.Admission.Application.Features.People.Queries;

/// <summary>
/// دریافت موجودیت شخص با کد یکتا
/// </summary>
public sealed record GetPersonByYektaCodeQuery(string YektaCode) : IRequest<ValidateNonIranianYektaCodeResponse>;

internal sealed class GetPersonByYektaCodeQueryHandler(
    IPersonRepository personRepo,
    ILogger<GetPersonByYektaCodeQueryHandler> logger)
    : IRequestHandler<GetPersonByYektaCodeQuery, ValidateNonIranianYektaCodeResponse>
{
    public async Task<ValidateNonIranianYektaCodeResponse> Handle(GetPersonByYektaCodeQuery request, CancellationToken cancellationToken) {

        if ( string.IsNullOrWhiteSpace(request.YektaCode) ) {
            throw new CommandValidationException("کد یکتا وارد نشده است.");
        }

        var person = await personRepo.GetOneAsync<PersonDto>(
            x => x.YektaCode == request.YektaCode,
            cancellationToken: cancellationToken);

        return person != null ? MapToResponse(person) : null;
    }

    /// <summary>
    /// بر اساس مدل بازگشتی از ثبت احوال
    /// </summary>
    /// <param name="person"></param>
    /// <returns></returns>
    private static ValidateNonIranianYektaCodeResponse MapToResponse(PersonDto person) =>
        new() {
            Id = person.Id,
            FatherName = person.FatherName,
            BirthDate = person.BirthDate.ToDateOnly().Value,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Gender = (int) person.Gender,
            PassportNumber = person.PassportNumber,
            NationalityName = person.Citizenship.GetDisplayName(),
            UniqeCode = person.YektaCode.ToLong(),
            IsRegistered = true
        };
}

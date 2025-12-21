using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Features.People.Dtos;

namespace Csis.Admission.Application.Features.People.Queries;

/// <summary>
/// دریافت موجودیت شخص با شناسه
/// </summary>
public sealed record GetPersonByIdQuery(string queryParam) : IRequest<PersonDto>;

internal sealed class GetPersonByIdQueryHandler(
    IPersonRepository personRepo,
    ILogger<GetPersonByIdQueryHandler> logger,
    IMapper mapper)
    : IRequestHandler<GetPersonByIdQuery, PersonDto>
{
    public async Task<PersonDto> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken) {
        logger.LogDebug("Executing scenario to get person with id {id}", request.queryParam);

        var person = await personRepo.GetPersonWithRelationAsync(request.queryParam);

        if ( person == null ) {
            logger.LogWarning("Person with id {id} not found", request.queryParam);
            throw new CommandValidationException($"اطلاعاتی با شناسه {request.queryParam} یافت نشد.");
        }

        var children = await personRepo.GetAllAsync(x => x.FatherPersonId == person.Id || x.MotherPersonId == person.Id, cancellationToken: cancellationToken);

        var personDto = mapper.Map<PersonDto>(person);
        personDto = personDto with { Relations = AddRelations(person, children) };

        logger.LogDebug("Successfully executed scenario for person with id {id}", request.queryParam);
        return personDto;
    }

    private List<PersonRelationsInfoDto> AddRelations(Person person, IEnumerable<Person> children) {
        var relations = new List<PersonRelationsInfoDto>();

        if ( person.FatherPerson is not null ) { relations.Add(ToRelationInfo(person.FatherPerson, FamilyRelationType.Father)); }

        if ( person.MotherPerson is not null ) { relations.Add(ToRelationInfo(person.MotherPerson, FamilyRelationType.Mother)); }

        if ( person.MarriageHusbandPeople is not null ) {
            relations.AddRange(
                person.MarriageHusbandPeople
                    .Where(m => m.WifePerson is not null)
                    .Select(m => ToRelationInfo(m.WifePerson, FamilyRelationType.Spouse)));
        }

        if ( person.MarriageWifePeople is not null ) {
            relations.AddRange(
                person.MarriageWifePeople
                    .Where(m => m.HusbandPerson is not null)
                    .Select(m => ToRelationInfo(m.HusbandPerson, FamilyRelationType.Spouse)));
        }

        if ( children is not null ) { relations.AddRange(children.Select(ch => ToRelationInfo(ch, FamilyRelationType.Child))); }

        return relations
            .GroupBy(x => new { x.FamilyRelationType, x.UniqueCode })
            .Select(g => g.First())
            .ToList();
    }

    private PersonRelationsInfoDto ToRelationInfo(Person p, FamilyRelationType rel) => mapper.Map<PersonRelationsInfoDto>(p) with { FamilyRelationType = rel };
}

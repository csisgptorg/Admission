//using Csis.Abstractions.Exceptions;
//using Csis.Admission.Application.Features.People.Commands;
//using Csis.Admission.Domain.Entities;
//using Csis.Admission.Domain.Enums;
//using Csis.Utilities;

//namespace Csis.Admission.IntegrationTests.Application.People;

//internal sealed class DeletePersonCommandTests : BaseTestFixture
//{
//    [Test]
//    public async Task Handle_WhenCalled_ShouldDeletePerson() {
//        var personId = (await CreatePersonAsync(fidaCode: StringHelper.Random(6), nationalCode: StringHelper.Random(6), yektaCode: StringHelper.Random(6))).Id;

//        personId.Should().BePositive();

//        await SendAsync(new DeletePersonCommand(personId));

//        var person = await FindAsync<Person>(personId);
//        person.Should().BeNull();
//    }

//    [TestCase(-1)]
//    [TestCase(0)]
//    [TestCase(int.MaxValue)]
//    public async Task HandleDelete_WhenPersonIdIsInvalid_ShouldThrowException(int id) {
//        await FluentActions
//            .Invoking(() => SendAsync(new DeletePersonCommand(id)))
//            .Should()
//            .ThrowAsync<RecordNotFoundException<Person>>();
//    }
//}

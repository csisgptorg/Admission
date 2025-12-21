//using Csis.Abstractions.Exceptions;
//using Csis.Admission.Application.Features.NonStudentDependants.Commands;
//using Csis.Admission.Domain.Entities;
//using Csis.Admission.Domain.Enums;
//using Csis.Utilities;

//namespace Csis.Admission.IntegrationTests.Application.NonStudentDependants;

//internal sealed class DeleteNonStudentDependantCommandTests : BaseTestFixture
//{
//    [Test]
//    public async Task Handle_WhenCalled_ShouldDeleteNonStudentDependant() {
//        var person = await CreatePersonAsync(fidaCode: "xpnlebmz", nationalCode: "hzszbowb", yektaCode: "jpiluyqg");
//        var nonStudentDependantId = (await CreateNonStudentDependantAsync(personId: person.Id, nonStudentCodm: 423720, isActive: true)).Id;

//        nonStudentDependantId.Should().BePositive();

//        await SendAsync(new DeleteNonStudentDependantCommand(nonStudentDependantId));

//        var nonStudentDependant = await FindAsync<NonStudentDependant>(nonStudentDependantId);
//        nonStudentDependant.Should().BeNull();
//    }

//    [TestCase(-1)]
//    [TestCase(0)]
//    [TestCase(int.MaxValue)]
//    public async Task HandleDelete_WhenNonStudentDependantIdIsInvalid_ShouldThrowException(int id) {
//        await FluentActions
//            .Invoking(() => SendAsync(new DeleteNonStudentDependantCommand(id)))
//            .Should()
//            .ThrowAsync<RecordNotFoundException<NonStudentDependant>>();
//    }
//}

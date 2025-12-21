//using Csis.Abstractions.Exceptions;
//using Csis.Admission.Application.Features.NonStudents.Commands;
//using Csis.Admission.Domain.Entities;
//using Csis.Utilities;

//namespace Csis.Admission.IntegrationTests.Application.NonStudents;

//internal sealed class DeleteNonStudentCommandTests : BaseTestFixture
//{
//    [Test]
//    public async Task Handle_WhenCalled_ShouldDeleteNonStudent() {
//        var person = await CreatePersonAsync(fidaCode: "hirwpxac", nationalCode: "eyftuinw", yektaCode: "fjrpkxgm");
//        var nonStudentId = (await CreateNonStudentAsync(personId: person.Id)).Id;

//        nonStudentId.Should().BePositive();

//        await SendAsync(new DeleteNonStudentCommand(nonStudentId));

//        var nonStudent = await FindAsync<NonStudent>(nonStudentId);
//        nonStudent.Should().BeNull();
//    }

//    [TestCase(-1)]
//    [TestCase(0)]
//    [TestCase(int.MaxValue)]
//    public async Task HandleDelete_WhenNonStudentIdIsInvalid_ShouldThrowException(int id) {
//        await FluentActions
//            .Invoking(() => SendAsync(new DeleteNonStudentCommand(id)))
//            .Should()
//            .ThrowAsync<RecordNotFoundException<NonStudent>>();
//    }
//}

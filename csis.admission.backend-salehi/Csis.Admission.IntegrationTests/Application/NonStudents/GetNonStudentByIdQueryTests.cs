//using Csis.Abstractions.Exceptions;
//using Csis.Admission.Application.Features.NonStudents.Dtos;
//using Csis.Admission.Application.Features.NonStudents.Queries;
//using Csis.Admission.Domain.Entities;
//using Csis.Utilities;

//namespace Csis.Admission.IntegrationTests.Application.NonStudents;

//internal sealed class GetNonStudentByIdQueryTests : BaseTestFixture
//{
//    private NonStudent _nonStudent;

//    [SetUp]
//    public async Task SetUp() {
//        var person = await CreatePersonAsync(fidaCode: "ceggehya", nationalCode: "ndvnchwz", yektaCode: "gyrvhmvl");
//        _nonStudent = await CreateNonStudentAsync(personId: person.Id);
//    }

//    [Test]
//    public async Task Handle_WhenCalled_ShouldReturnNonStudent() {
//        var nonStudent = await SendAsync(new GetNonStudentByIdQuery(_nonStudent.Id));

//        nonStudent.Should().NotBeNull();
//        nonStudent.Should().BeOfType<NonStudentDto>();
//        nonStudent.Id.Should().Be(_nonStudent.Id);
//        nonStudent.PersonId.Should().Be(_nonStudent.PersonId);
//        nonStudent.Agency.Should().Be(_nonStudent.Agency);
//        nonStudent.Branch.Should().Be(_nonStudent.Branch);
//        nonStudent.CaseBlockDate.Should().Be(_nonStudent.CaseBlockDate);
//        nonStudent.CaseCreateDate.Should().Be(_nonStudent.CaseCreateDate);
//        nonStudent.CaseExpireDate.Should().Be(_nonStudent.CaseExpireDate);
//        nonStudent.Status.Should().Be(_nonStudent.Status);
//        nonStudent.Type.Should().Be(_nonStudent.Type);
//    }

//    [TestCase(-1)]
//    [TestCase(0)]
//    [TestCase(int.MaxValue)]
//    public async Task HandleGetById_WhenNonStudentIdIsInvalid_ShouldThrowException(int id) {
//        await FluentActions
//            .Invoking(() => SendAsync(new GetNonStudentByIdQuery(id)))
//            .Should()
//            .ThrowAsync<RecordNotFoundException<NonStudent>>();
//    }
//}

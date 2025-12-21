//using Csis.Abstractions.Exceptions;
//using Csis.Admission.Application.Features.NonStudents.Commands;
//using Csis.Admission.Application.Features.NonStudents.Validators;
//using Csis.Admission.Domain.Entities;
//using Csis.Admission.Domain.Enums;
//using Csis.Utilities;
//using FluentValidation.TestHelper;

//namespace Csis.Admission.IntegrationTests.Application.NonStudents;

//internal sealed class UpdateNonStudentCommandTests : BaseTestFixture
//{
//    [Test]
//    public async Task Handle_WhenCalled_ShouldUpdateNonStudent() {
//        var person = await CreatePersonAsync(fidaCode: "znpfavmt", nationalCode: "vbtwcofh", yektaCode: "opyfraiu");
//        var person2 = await CreatePersonAsync(fidaCode: "zxkvbiid", nationalCode: "nrhzfhtk", yektaCode: "tdkimnbt");
//        var nonStudentId = (await CreateNonStudentAsync(personId: person.Id)).Id;

//        nonStudentId.Should().BePositive();

//        var command = new UpdateNonStudentCommand {
//            Id = nonStudentId,
//            PersonId = person2.Id,
//            Agency = new byte?(),
//            Branch = new byte?(),
//            CaseBlockDate = new DateOnly?(),
//            CaseCreateDate = new DateOnly(),
//            CaseExpireDate = new DateOnly?(),
//            Status = NonStudentStatus.Active,
//            Type = new NonStudentType?(),
//        };

//        await SendAsync(command);

//        var nonStudent = await FindAsync<NonStudent>(nonStudentId);

//        nonStudent.Id.Should().Be(nonStudentId);
//        nonStudent.PersonId.Should().Be(person2.Id);
//        nonStudent.Agency.Should().Be(new byte?());
//        nonStudent.Branch.Should().Be(new byte?());
//        nonStudent.CaseBlockDate.Should().Be(new DateOnly?());
//        nonStudent.CaseCreateDate.Should().Be(new DateOnly());
//        nonStudent.CaseExpireDate.Should().Be(new DateOnly?());
//        nonStudent.Status.Should().Be(command.Status);
//        nonStudent.Type.Should().Be(new NonStudentType?());
//        nonStudent.UpdatedOn.Should().NotBeNull();
//        nonStudent.LastUpdatedById.Should().NotBeNull();
//        nonStudent.DeletedOn.Should().Be(null);
//        nonStudent.DeletedById.Should().Be(null);
//        nonStudent.Deleted.Should().BeFalse();
//    }

//    [TestCase(-1)]
//    [TestCase(0)]
//    [TestCase(int.MaxValue)]
//    public async Task HandleUpdate_WhenPersonIdIsInvalid_ShouldThrowCommandValidationException(int personId) {
//        var person = await CreatePersonAsync(fidaCode: "znpfavmt", nationalCode: "vbtwcofh", yektaCode: "opyfraiu");
//        var command = new CreateNonStudentCommand {
//            PersonId = person.Id,
//            //Agency = new byte?(),
//            //Branch = new byte?(),
//            Status = NonStudentStatus.Active,
//            Type = new NonStudentType?(),
//        };

//        var id = await SendAsync(command);

//        var command2 = new UpdateNonStudentCommand {
//            Id = id,
//            PersonId = personId,
//            Agency = new byte?(),
//            Branch = new byte?(),
//            CaseBlockDate = new DateOnly?(),
//            CaseCreateDate = new DateOnly(),
//            CaseExpireDate = new DateOnly?(),
//            Status = NonStudentStatus.Active,
//            Type = new NonStudentType?(),
//        };

//        await FluentActions
//            .Invoking(() => SendAsync(command2))
//            .Should()
//            .ThrowAsync<CommandValidationException>()
//            .WithMessage("شخس انتخاب شده نامعتبر است");
//    }

//    [TestCase(-1)]
//    [TestCase(0)]
//    [TestCase(int.MaxValue)]
//    public async Task HandleUpdate_WhenNonStudentIdIsInvalid_ShouldThrowException(int id) {
//        await FluentActions
//            .Invoking(() => SendAsync(new UpdateNonStudentCommand { Id = id }))
//            .Should()
//            .ThrowAsync<RecordNotFoundException<NonStudent>>();
//    }

//    [Test]
//    public async Task Handle_WhenUpdateNonStudentCommandInputIsInvalid_ShouldHaveValidationError() {
//        var command = new UpdateNonStudentCommand();
//        var validator = new UpdateNonStudentCommandValidator();

//        var result = await validator.TestValidateAsync(command);

//    }
//}

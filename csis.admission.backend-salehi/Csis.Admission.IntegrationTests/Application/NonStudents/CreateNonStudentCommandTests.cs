//using Csis.Abstractions.Exceptions;
//using Csis.Admission.Application.Features.NonStudents.Commands;
//using Csis.Admission.Application.Features.NonStudents.Validators;
//using Csis.Admission.Domain.Entities;
//using Csis.Admission.Domain.Enums;
//using Csis.Utilities;
//using FluentValidation.TestHelper;

//namespace Csis.Admission.IntegrationTests.Application.NonStudents;

//internal sealed class CreateNonStudentCommandTests : BaseTestFixture
//{
//    [Test]
//    public async Task Handle_WhenCalled_ShouldCreateNonStudent() {
//        var person = await CreatePersonAsync(fidaCode: "kpfzymiu", nationalCode: "lzjqlhzp", yektaCode: "jrsfqzky");
//        var command = new CreateNonStudentCommand {
//            PersonId = person.Id,
//            //Agency = new byte?(),
//            //Branch = new byte?(),
//            Status = NonStudentStatus.Active,
//            //Type = NonStudentType.,
//        };

//        var id = await SendAsync(command);

//        var nonStudent = await FindAsync<NonStudent>(id);

//        nonStudent.PersonId.Should().Be(person.Id);
//        nonStudent.Agency.Should().Be(new byte?());
//        nonStudent.Branch.Should().Be(new byte?());
//        nonStudent.CaseBlockDate.Should().Be(new DateOnly?());
//        nonStudent.CaseCreateDate.Should().Be(new DateOnly());
//        nonStudent.CaseExpireDate.Should().Be(new DateOnly?());
//        nonStudent.Status.Should().Be(command.Status);
//        nonStudent.Type.Should().Be(new NonStudentType?());
//        nonStudent.UpdatedOn.Should().Be(null);
//        nonStudent.LastUpdatedById.Should().Be(null);
//        nonStudent.DeletedOn.Should().Be(null);
//        nonStudent.DeletedById.Should().Be(null);
//        nonStudent.Deleted.Should().BeFalse();
//    }

//    [TestCase(-1)]
//    [TestCase(0)]
//    [TestCase(int.MaxValue)]
//    public async Task HandleCreate_WhenPersonIdIsInvalid_ShouldThrowCommandValidationException(int personId) {
//        var person = await CreatePersonAsync(fidaCode: "kpfzymiu", nationalCode: "lzjqlhzp", yektaCode: "jrsfqzky");
//        var command = new CreateNonStudentCommand {
//            PersonId = personId,
//            //Agency = new byte?(),
//            //Branch = new byte?(),
//            Status = NonStudentStatus.Active,
//            Type = new NonStudentType?(),
//        };

//        await FluentActions
//            .Invoking(() => SendAsync(command))
//            .Should()
//            .ThrowAsync<CommandValidationException>()
//            .WithMessage("شخس انتخاب شده نامعتبر است");
//    }

//    [Test]
//    public async Task Handle_WhenCreateNonStudentCommandInputIsInvalid_ShouldHaveValidationError() {
//        var command = new CreateNonStudentCommand();
//        var validator = new CreateNonStudentCommandValidator();

//        var result = await validator.TestValidateAsync(command);

//    }
//}

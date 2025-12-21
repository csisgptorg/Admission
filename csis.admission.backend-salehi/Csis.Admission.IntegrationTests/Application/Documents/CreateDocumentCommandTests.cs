//using Csis.Abstractions.Exceptions;
//using Csis.Admission.Application.Features.Documents.Commands;
//using Csis.Admission.Application.Features.Documents.Validators;
//using Csis.Admission.Domain.Entities;
//using Csis.Admission.Domain.Enums;
//using Csis.Utilities;
//using FluentValidation.TestHelper;

//namespace Csis.Admission.IntegrationTests.Application.Documents;

//internal sealed class CreateDocumentCommandTests : BaseTestFixture
//{
//    [Test]
//    public async Task Handle_WhenCalled_ShouldCreateDocument() {
//        var person = await CreatePersonAsync(fidaCode: "rzwqtbko", nationalCode: "ydrsnttk", yektaCode: "hyteqxec");
//        var command = new CreateDocumentCommand {
//            FileIdentifier = "aapwmzpm",
//            NonStudentCodm = 93150, //TODO: Change this to use correct foreign key
//            NonStudentDependantId = 63775, //TODO: Change this to use correct foreign key
//            PersonId = person.Id,
//            StudentCodm = 80312, //TODO: Change this to use correct foreign key
//            StudentDependantId = 99388, //TODO: Change this to use correct foreign key
//            TableId = 41796,
//            TableRecordId = 21743,
//            Type = 0,
//        };

//        var id = await SendAsync(command);

//        var document = await FindAsync<Document>(id);

//        document.FileIdentifier.Should().Be("aapwmzpm");
//        document.NonStudentCodm.Should().Be(93150);
//        document.NonStudentDependantId.Should().Be(63775);
//        document.PersonId.Should().Be(person.Id);
//        document.StudentCodm.Should().Be(80312);
//        document.StudentDependantId.Should().Be(99388);
//        document.TableId.Should().Be(41796);
//        document.TableRecordId.Should().Be(21743);
//        document.Type.Should().Be(0);
//        document.UpdatedOn.Should().Be(null);
//        document.LastUpdatedById.Should().Be(null);
//        document.DeletedOn.Should().Be(null);
//        document.DeletedById.Should().Be(null);
//        document.Deleted.Should().BeFalse();
//    }

//    [Test]
//    public async Task HandleCreate_WhenDocumentFileIdentifierIsDuplicate_ShouldThrowCommandValidationException() {
//        var person = await CreatePersonAsync(fidaCode: "rzwqtbko", nationalCode: "ydrsnttk", yektaCode: "hyteqxec");
//        var command = new CreateDocumentCommand {
//            FileIdentifier = "aapwmzpm",
//            NonStudentCodm = 93150, //TODO: Change this to use correct foreign key
//            NonStudentDependantId = 63775, //TODO: Change this to use correct foreign key
//            PersonId = person.Id,
//            StudentCodm = 80312, //TODO: Change this to use correct foreign key
//            StudentDependantId = 99388, //TODO: Change this to use correct foreign key
//            TableId = 41796,
//            TableRecordId = 21743,
//            Type = 0,
//        };

//        var id = await SendAsync(command);

//        var command2 = new CreateDocumentCommand {
//            FileIdentifier = "aapwmzpm",
//            NonStudentCodm = 89142, //TODO: Change this to use correct foreign key
//            NonStudentDependantId = 94568, //TODO: Change this to use correct foreign key
//            PersonId = person.Id,
//            StudentCodm = 47693, //TODO: Change this to use correct foreign key
//            StudentDependantId = 8177, //TODO: Change this to use correct foreign key
//            TableId = 22764,
//            TableRecordId = 42387,
//            Type = 0,
//        };

//        await FluentActions
//            .Invoking(() => SendAsync(command2))
//            .Should()
//            .ThrowAsync<CommandValidationException>()
//            .WithMessage("شناسه فایل وارد شده تکراری است");
//    }

//    [Test]
//    public async Task Handle_WhenCreateDocumentCommandInputIsInvalid_ShouldHaveValidationError() {
//        var command = new CreateDocumentCommand();
//        var validator = new CreateDocumentCommandValidator();

//        var result = await validator.TestValidateAsync(command);

//        result.ShouldHaveValidationErrorFor(x => x.FileIdentifier);
//    }
//}

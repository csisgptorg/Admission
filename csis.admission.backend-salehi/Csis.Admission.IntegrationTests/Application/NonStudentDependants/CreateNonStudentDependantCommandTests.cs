//using Csis.Abstractions.Exceptions;
//using Csis.Admission.Application.Features.NonStudentDependants.Commands;
//using Csis.Admission.Application.Features.NonStudentDependants.Validators;
//using Csis.Admission.Domain.Entities;
//using Csis.Admission.Domain.Enums;
//using Csis.Utilities;
//using FluentValidation.TestHelper;

//namespace Csis.Admission.IntegrationTests.Application.NonStudentDependants;

//internal sealed class CreateNonStudentDependantCommandTests : BaseTestFixture
//{
//    [Test]
//    public async Task Handle_WhenCalled_ShouldCreateNonStudentDependant() {
//        var person = await CreatePersonAsync(fidaCode: "poxpygod", nationalCode: "grgllrfk", yektaCode: "cpszixig");
//        var command = new CreateNonStudentDependantCommand {
//            PersonId = person.Id,
//            NonStudentCodm = 171899, //TODO: Change this to use correct foreign key
//            IsActive = false,
//            Relationship = 0,
//            Branch = new byte?(),
//            CaseCreateDate = new DateOnly(),
//            CaseDeactiveDate = new DateOnly?(),
//            RelationshipOrder = new byte(),
//            StatusReason = new byte?(),
//        };

//        var id = await SendAsync(command);

//        var nonStudentDependant = await FindAsync<NonStudentDependant>(id);

//        nonStudentDependant.PersonId.Should().Be(person.Id);
//        nonStudentDependant.NonStudentCodm.Should().Be(171899);
//        nonStudentDependant.IsActive.Should().Be(false);
//        nonStudentDependant.Relationship.Should().Be(0);
//        nonStudentDependant.Branch.Should().Be(new byte?());
//        nonStudentDependant.CaseCreateDate.Should().Be(new DateOnly());
//        nonStudentDependant.CaseDeactiveDate.Should().Be(new DateOnly?());
//        nonStudentDependant.RelationshipOrder.Should().Be(new byte());
//        nonStudentDependant.StatusReason.Should().Be(new byte?());
//        nonStudentDependant.UpdatedOn.Should().Be(null);
//        nonStudentDependant.LastUpdatedById.Should().Be(null);
//        nonStudentDependant.DeletedOn.Should().Be(null);
//        nonStudentDependant.DeletedById.Should().Be(null);
//        nonStudentDependant.Deleted.Should().BeFalse();
//    }

//    [TestCase(-1)]
//    [TestCase(0)]
//    [TestCase(int.MaxValue)]
//    public async Task HandleCreate_WhenPersonIdIsInvalid_ShouldThrowCommandValidationException(int personId) {
//        var person = await CreatePersonAsync(fidaCode: "poxpygod", nationalCode: "grgllrfk", yektaCode: "cpszixig");
//        var command = new CreateNonStudentDependantCommand {
//            PersonId = personId,
//            NonStudentCodm = 171899, //TODO: Change this to use correct foreign key
//            IsActive = false,
//            Relationship = 0,
//            Branch = new byte?(),
//            CaseCreateDate = new DateOnly(),
//            CaseDeactiveDate = new DateOnly?(),
//            RelationshipOrder = new byte(),
//            StatusReason = new byte?(),
//        };

//        await FluentActions
//            .Invoking(() => SendAsync(command))
//            .Should()
//            .ThrowAsync<CommandValidationException>()
//            .WithMessage("شخس انتخاب شده نامعتبر است");
//    }

//    [Test]
//    public async Task Handle_WhenCreateNonStudentDependantCommandInputIsInvalid_ShouldHaveValidationError() {
//        var command = new CreateNonStudentDependantCommand();
//        var validator = new CreateNonStudentDependantCommandValidator();

//        var result = await validator.TestValidateAsync(command);

//    }
//}

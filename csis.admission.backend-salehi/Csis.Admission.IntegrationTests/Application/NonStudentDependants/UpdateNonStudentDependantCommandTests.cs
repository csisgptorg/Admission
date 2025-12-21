//using Csis.Abstractions.Exceptions;
//using Csis.Admission.Application.Features.NonStudentDependants.Commands;
//using Csis.Admission.Application.Features.NonStudentDependants.Validators;
//using Csis.Admission.Domain.Entities;
//using Csis.Admission.Domain.Enums;
//using Csis.Utilities;
//using FluentValidation.TestHelper;

//namespace Csis.Admission.IntegrationTests.Application.NonStudentDependants;

//internal sealed class UpdateNonStudentDependantCommandTests : BaseTestFixture
//{
//    [Test]
//    public async Task Handle_WhenCalled_ShouldUpdateNonStudentDependant() {
//        var person = await CreatePersonAsync(fidaCode: "rifzhbif", nationalCode: "iusrhaea", yektaCode: "ubqgyctq");
//        var person2 = await CreatePersonAsync(fidaCode: "orowquan", nationalCode: "pycwhqoa", yektaCode: "qbemnhfj");
//        var nonStudentDependantId = (await CreateNonStudentDependantAsync(personId: person.Id, nonStudentCodm: 50482, isActive: true)).Id;

//        nonStudentDependantId.Should().BePositive();

//        var command = new UpdateNonStudentDependantCommand {
//            Id = nonStudentDependantId,
//            PersonId = person2.Id,
//            NonStudentCodm = 389103, //TODO: Change this to use correct foreign key
//            IsActive = false,
//            Relationship = 0,
//            Branch = new byte?(),
//            CaseCreateDate = new DateOnly(),
//            CaseDeactiveDate = new DateOnly?(),
//            RelationshipOrder = new byte(),
//            StatusReason = new byte?(),
//        };

//        await SendAsync(command);

//        var nonStudentDependant = await FindAsync<NonStudentDependant>(nonStudentDependantId);

//        nonStudentDependant.Id.Should().Be(nonStudentDependantId);
//        nonStudentDependant.PersonId.Should().Be(person2.Id);
//        nonStudentDependant.NonStudentCodm.Should().Be(389103);
//        nonStudentDependant.IsActive.Should().Be(false);
//        nonStudentDependant.Relationship.Should().Be(0);
//        nonStudentDependant.Branch.Should().Be(new byte?());
//        nonStudentDependant.CaseCreateDate.Should().Be(new DateOnly());
//        nonStudentDependant.CaseDeactiveDate.Should().Be(new DateOnly?());
//        nonStudentDependant.RelationshipOrder.Should().Be(new byte());
//        nonStudentDependant.StatusReason.Should().Be(new byte?());
//        nonStudentDependant.UpdatedOn.Should().NotBeNull();
//        nonStudentDependant.LastUpdatedById.Should().NotBeNull();
//        nonStudentDependant.DeletedOn.Should().Be(null);
//        nonStudentDependant.DeletedById.Should().Be(null);
//        nonStudentDependant.Deleted.Should().BeFalse();
//    }

//    [TestCase(-1)]
//    [TestCase(0)]
//    [TestCase(int.MaxValue)]
//    public async Task HandleUpdate_WhenPersonIdIsInvalid_ShouldThrowCommandValidationException(int personId) {
//        var person = await CreatePersonAsync(fidaCode: "rifzhbif", nationalCode: "iusrhaea", yektaCode: "ubqgyctq");
//        var command = new CreateNonStudentDependantCommand {
//            PersonId = person.Id,
//            NonStudentCodm = 123884, //TODO: Change this to use correct foreign key
//            IsActive = false,
//            Relationship = 0,
//            Branch = new byte?(),
//            CaseCreateDate = new DateOnly(),
//            CaseDeactiveDate = new DateOnly?(),
//            RelationshipOrder = new byte(),
//            StatusReason = new byte?(),
//        };

//        var id = await SendAsync(command);

//        var command2 = new UpdateNonStudentDependantCommand {
//            Id = id,
//            PersonId = personId,
//            NonStudentCodm = 504412, //TODO: Change this to use correct foreign key
//            IsActive = false,
//            Relationship = 0,
//            Branch = new byte?(),
//            CaseCreateDate = new DateOnly(),
//            CaseDeactiveDate = new DateOnly?(),
//            RelationshipOrder = new byte(),
//            StatusReason = new byte?(),
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
//    public async Task HandleUpdate_WhenNonStudentDependantIdIsInvalid_ShouldThrowException(int id) {
//        await FluentActions
//            .Invoking(() => SendAsync(new UpdateNonStudentDependantCommand { Id = id }))
//            .Should()
//            .ThrowAsync<RecordNotFoundException<NonStudentDependant>>();
//    }

//    [Test]
//    public async Task Handle_WhenUpdateNonStudentDependantCommandInputIsInvalid_ShouldHaveValidationError() {
//        var command = new UpdateNonStudentDependantCommand();
//        var validator = new UpdateNonStudentDependantCommandValidator();

//        var result = await validator.TestValidateAsync(command);

//    }
//}

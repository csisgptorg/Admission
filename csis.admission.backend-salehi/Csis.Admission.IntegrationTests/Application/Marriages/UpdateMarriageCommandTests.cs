//using Csis.Abstractions.Exceptions;
//using Csis.Admission.Application.Features.Marriages.Commands;
//using Csis.Admission.Application.Features.Marriages.Validators;
//using Csis.Admission.Domain.Entities;
//using Csis.Utilities;
//using FluentValidation.TestHelper;

//namespace Csis.Admission.IntegrationTests.Application.Marriages;

//internal sealed class UpdateMarriageCommandTests : BaseTestFixture
//{
//    [Test]
//    public async Task Handle_WhenCalled_ShouldUpdateMarriage() {
//        var marriageId = (await CreateMarriageAsync()).Id;

//        marriageId.Should().BePositive();

//        var command = new UpdateMarriageCommand {
//            Id = marriageId,
//            HusbandPersonId = 31447, //TODO: Change this to use correct foreign key
//            WifePersonId = 18833, //TODO: Change this to use correct foreign key
//            DeathDate = new DateOnly?(),
//            DivorceDate = new DateOnly?(),
//            MarriageDate = new DateOnly?(),
//        };

//        await SendAsync(command);

//        var marriage = await FindAsync<Marriage>(marriageId);

//        marriage.Id.Should().Be(marriageId);
//        marriage.HusbandPersonId.Should().Be(31447);
//        marriage.WifePersonId.Should().Be(18833);
//        marriage.DeathDate.Should().Be(new DateOnly?());
//        marriage.DivorceDate.Should().Be(new DateOnly?());
//        marriage.MarriageDate.Should().Be(new DateOnly?());
//        marriage.UpdatedOn.Should().NotBeNull();
//        marriage.LastUpdatedById.Should().NotBeNull();
//        marriage.DeletedOn.Should().Be(null);
//        marriage.DeletedById.Should().Be(null);
//        marriage.Deleted.Should().BeFalse();
//    }

//    [TestCase(-1)]
//    [TestCase(0)]
//    [TestCase(int.MaxValue)]
//    public async Task HandleUpdate_WhenHusbandPersonIdIsInvalid_ShouldThrowCommandValidationException(int husbandPersonId) {
//        var command = new CreateMarriageCommand {
//            HusbandPersonId = 19846, //TODO: Change this to use correct foreign key
//            WifePersonId = 1142, //TODO: Change this to use correct foreign key
//            DeathDate = new DateOnly?(),
//            DivorceDate = new DateOnly?(),
//            MarriageDate = new DateOnly?(),
//        };

//        var id = await SendAsync(command);

//        var command2 = new UpdateMarriageCommand {
//            Id = id,
//            HusbandPersonId = husbandPersonId,
//            WifePersonId = 63795, //TODO: Change this to use correct foreign key
//            DeathDate = new DateOnly?(),
//            DivorceDate = new DateOnly?(),
//            MarriageDate = new DateOnly?(),
//        };

//        await FluentActions
//            .Invoking(() => SendAsync(command2))
//            .Should()
//            .ThrowAsync<CommandValidationException>()
//            .WithMessage("شوهر انتخاب شده نامعتبر است");
//    }

//    [TestCase(-1)]
//    [TestCase(0)]
//    [TestCase(int.MaxValue)]
//    public async Task HandleUpdate_WhenWifePersonIdIsInvalid_ShouldThrowCommandValidationException(int wifePersonId) {
//        var command = new CreateMarriageCommand {
//            HusbandPersonId = 41505, //TODO: Change this to use correct foreign key
//            WifePersonId = 53968, //TODO: Change this to use correct foreign key
//            DeathDate = new DateOnly?(),
//            DivorceDate = new DateOnly?(),
//            MarriageDate = new DateOnly?(),
//        };

//        var id = await SendAsync(command);

//        var command2 = new UpdateMarriageCommand {
//            Id = id,
//            HusbandPersonId = 36173, //TODO: Change this to use correct foreign key
//            WifePersonId = wifePersonId,
//            DeathDate = new DateOnly?(),
//            DivorceDate = new DateOnly?(),
//            MarriageDate = new DateOnly?(),
//        };

//        await FluentActions
//            .Invoking(() => SendAsync(command2))
//            .Should()
//            .ThrowAsync<CommandValidationException>()
//            .WithMessage("همسر انتخاب شده نامعتبر است");
//    }

//    [TestCase(-1)]
//    [TestCase(0)]
//    [TestCase(int.MaxValue)]
//    public async Task HandleUpdate_WhenMarriageIdIsInvalid_ShouldThrowException(int id) {
//        await FluentActions
//            .Invoking(() => SendAsync(new UpdateMarriageCommand { Id = id }))
//            .Should()
//            .ThrowAsync<RecordNotFoundException<Marriage>>();
//    }

//    [Test]
//    public async Task Handle_WhenUpdateMarriageCommandInputIsInvalid_ShouldHaveValidationError() {
//        var command = new UpdateMarriageCommand();
//        var validator = new UpdateMarriageCommandValidator();

//        var result = await validator.TestValidateAsync(command);

//    }
//}

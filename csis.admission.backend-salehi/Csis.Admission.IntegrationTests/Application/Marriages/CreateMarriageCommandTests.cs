using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Features.Marriages.Commands;
using Csis.Admission.Application.Features.Marriages.Validators;
using Csis.Admission.Domain.Entities;
using Csis.Utilities;
using FluentValidation.TestHelper;

namespace Csis.Admission.IntegrationTests.Application.Marriages;

internal sealed class CreateMarriageCommandTests : BaseTestFixture
{
    [Test]
    public async Task Handle_WhenCalled_ShouldCreateMarriage() {
        var command = new CreatePersonMarriageCommand {
            HusbandPersonId = 16, //TODO: Change this to use correct foreign key
            WifePersonId = 46226, //TODO: Change this to use correct foreign key
            DeathDate = new DateOnly?(),
            DivorceDate = new DateOnly?(),
            MarriageDate = new DateOnly?(),
        };

        var id = await SendAsync(command);

        var marriage = await FindAsync<Marriage>(id);

        marriage.HusbandPersonId.Should().Be(16);
        marriage.WifePersonId.Should().Be(46226);
        marriage.DeathDate.Should().Be(new DateOnly?());
        marriage.DivorceDate.Should().Be(new DateOnly?());
        marriage.MarriageDate.Should().Be(new DateOnly?());
        marriage.UpdatedOn.Should().Be(null);
        marriage.LastUpdatedById.Should().Be(null);
        marriage.DeletedOn.Should().Be(null);
        marriage.DeletedById.Should().Be(null);
        marriage.Deleted.Should().BeFalse();
    }

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(int.MaxValue)]
    public async Task HandleCreate_WhenHusbandPersonIdIsInvalid_ShouldThrowCommandValidationException(int husbandPersonId) {
        var command = new CreatePersonMarriageCommand {
            HusbandPersonId = husbandPersonId, //TODO: Change this to use correct foreign key
            WifePersonId = 46226, //TODO: Change this to use correct foreign key
            DeathDate = new DateOnly?(),
            DivorceDate = new DateOnly?(),
            MarriageDate = new DateOnly?(),
        };

        await FluentActions
            .Invoking(() => SendAsync(command))
            .Should()
            .ThrowAsync<CommandValidationException>()
            .WithMessage("شوهر انتخاب شده نامعتبر است");
    }

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(int.MaxValue)]
    public async Task HandleCreate_WhenWifePersonIdIsInvalid_ShouldThrowCommandValidationException(int wifePersonId) {
        var command = new CreatePersonMarriageCommand {
            HusbandPersonId = 16, //TODO: Change this to use correct foreign key
            WifePersonId = wifePersonId, //TODO: Change this to use correct foreign key
            DeathDate = new DateOnly?(),
            DivorceDate = new DateOnly?(),
            MarriageDate = new DateOnly?(),
        };

        await FluentActions
            .Invoking(() => SendAsync(command))
            .Should()
            .ThrowAsync<CommandValidationException>()
            .WithMessage("همسر انتخاب شده نامعتبر است");
    }

    [Test]
    public async Task Handle_WhenCreateMarriageCommandInputIsInvalid_ShouldHaveValidationError() {
        var command = new CreatePersonMarriageCommand();
        var validator = new CreateMarriageCommandValidator();

        var result = await validator.TestValidateAsync(command);

    }
}

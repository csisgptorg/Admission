using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Features.Famous.Commands;
using Csis.Admission.Application.Features.Famous.Validators;
using Csis.Admission.Domain.Entities;
using Csis.Utilities;
using FluentValidation.TestHelper;

namespace Csis.Admission.IntegrationTests.Application.Famous;

internal sealed class UpdateFamousCommandTests : BaseTestFixture
{
    [Test]
    public async Task Handle_WhenCalled_ShouldUpdateFamous() {
        var famousId = (await CreateFamousAsync()).Id;

        famousId.Should().BePositive();

        var command = new UpdateFamousCommand {
            Id = famousId,
            ActionPlace = "epubdpzu",
            Position = "yjmohjkb",
            Codm = 29799,
            Area = new AreaEnum(),
            Role = new RoleEnum?(),
            Type = new TypeEnum(),
        };

        await SendAsync(command);

        var famous = await FindAsync<Famous>(famousId);

        famous.Id.Should().Be(famousId);
        famous.ActionPlace.Should().Be("epubdpzu");
        famous.Position.Should().Be("yjmohjkb");
        famous.Codm.Should().Be(29799);
        famous.Area.Should().Be(new AreaEnum());
        famous.Role.Should().Be(new RoleEnum?());
        famous.Type.Should().Be(new TypeEnum());
        famous.UpdatedOn.Should().NotBeNull();
        famous.LastUpdatedById.Should().NotBeNull();
        famous.DeletedOn.Should().Be(null);
        famous.DeletedById.Should().Be(null);
        famous.Deleted.Should().BeFalse();
    }

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(int.MaxValue)]
    public async Task HandleUpdate_WhenFamousIdIsInvalid_ShouldThrowException(int id) {
        await FluentActions
            .Invoking(() => SendAsync(new UpdateFamousCommand { Id = id }))
            .Should()
            .ThrowAsync<RecordNotFoundException<Famous>>();
    }

    [Test]
    public async Task Handle_WhenUpdateFamousCommandInputIsInvalid_ShouldHaveValidationError() {
        var command = new UpdateFamousCommand();
        var validator = new UpdateFamousCommandValidator();

        var result = await validator.TestValidateAsync(command);

    }
}

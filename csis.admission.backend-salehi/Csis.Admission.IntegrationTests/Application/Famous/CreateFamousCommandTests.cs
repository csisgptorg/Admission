using Csis.Admission.Application.Features.Famouses.Commands;
using Csis.Admission.Application.Features.Famouses.Validators;
using Csis.Admission.Domain.Enums;
using FluentValidation.TestHelper;

namespace Csis.Admission.IntegrationTests.Application.Famous;

internal sealed class CreateFamousCommandTests : BaseTestFixture
{
    [Test]
    public async Task Handle_WhenCalled_ShouldCreateFamous() {
        var command = new CreateFamousCommand {
            Codm = 50946,
            Area = new AreaEnum(),
            Role = new RoleEnum?(),
            Type = new TypeEnum(),
        };

        var id = await SendAsync(command);

        var famous = await FindAsync<Domain.Entities.Famous>(id);

        famous.Codm.Should().Be(50946);
        famous.Area.Should().Be(new AreaEnum());
        famous.Role.Should().Be(new RoleEnum?());
        famous.Type.Should().Be(new TypeEnum());
        famous.UpdatedOn.Should().Be(null);
        famous.LastUpdatedById.Should().Be(null);
        famous.DeletedOn.Should().Be(null);
        famous.DeletedById.Should().Be(null);
        famous.Deleted.Should().BeFalse();
    }

    [Test]
    public async Task Handle_WhenCreateFamousCommandInputIsInvalid_ShouldHaveValidationError() {
        var command = new CreateFamousCommand();
        var validator = new CreateFamousCommandValidator();

        var result = await validator.TestValidateAsync(command);

    }
}

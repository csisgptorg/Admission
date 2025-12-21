using Csis.Admission.Application.Features.DependentCaseActive.Commands;
using Csis.Admission.Application.Features.Famouses.Commands;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Domain.Enums;

namespace Csis.Admission.IntegrationTests.Application.Case;

internal sealed class OpenDependentCaseAsyncCommandTests : BaseTestFixture
{
    [Test]
    public async Task Handle_WhenCalled_ShouldOpenDependentCase() {
        var command = new AutomaticOpenDependentCaseRequestCommand(82000, 969800);

        var id = await SendAsync(command);

        var request = await FindAsync<Request>(id);

        request.Codm.Should().Be(82000);
        request.DependentId.Should().Be(969800);
        request.Type.Should().Be(RequestType.AutomaticOpenDependentCase);
        request.Source.Should().Be(DataSource.Employee);


        request.UpdatedOn.Should().Be(null);
        request.LastUpdatedById.Should().Be(null);
        request.DeletedOn.Should().Be(null);
        request.DeletedById.Should().Be(null);
        request.Deleted.Should().BeFalse();
    }

    [Test]
    public async Task Handle_WhenDependentIsDead_ShouldHaveValidationError() {
        // Test implementation for validation error when dependent is dead
    }
}

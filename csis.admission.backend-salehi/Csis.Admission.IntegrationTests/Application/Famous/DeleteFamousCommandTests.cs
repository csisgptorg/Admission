using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Features.Famous.Commands;
using Csis.Admission.Domain.Entities;
using Csis.Utilities;

namespace Csis.Admission.IntegrationTests.Application.Famous;

internal sealed class DeleteFamousCommandTests : BaseTestFixture
{
    [Test]
    public async Task Handle_WhenCalled_ShouldDeleteFamous() {
        var famousId = (await CreateFamousAsync()).Id;

        famousId.Should().BePositive();

        await SendAsync(new DeleteFamousCommand(famousId));

        var famous = await FindAsync<Famous>(famousId);
        famous.Should().BeNull();
    }

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(int.MaxValue)]
    public async Task HandleDelete_WhenFamousIdIsInvalid_ShouldThrowException(int id) {
        await FluentActions
            .Invoking(() => SendAsync(new DeleteFamousCommand(id)))
            .Should()
            .ThrowAsync<RecordNotFoundException<Famous>>();
    }
}

using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Features.Researches.Commands;
using Csis.Admission.Domain.Entities;
using Csis.Utilities;

namespace Csis.Admission.IntegrationTests.Application.Researches;

internal sealed class DeleteResearchCommandTests : BaseTestFixture
{
    [Test]
    public async Task Handle_WhenCalled_ShouldDeleteResearch() {
        var researchId = (await CreateResearchAsync()).Id;

        researchId.Should().BePositive();

        await SendAsync(new DeleteResearchCommand(researchId));

        var research = await FindAsync<Research>(researchId);
        research.Should().BeNull();
    }

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(int.MaxValue)]
    public async Task HandleDelete_WhenResearchIdIsInvalid_ShouldThrowException(int id) {
        await FluentActions
            .Invoking(() => SendAsync(new DeleteResearchCommand(id)))
            .Should()
            .ThrowAsync<RecordNotFoundException<Research>>();
    }
}

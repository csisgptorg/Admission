using Csis.Admission.Application.Features.DependentActiveReasons.Dtos;
using Csis.Admission.Application.Features.DependentActiveReasons.Queries;
using Csis.Paging;
using Csis.Utilities;

namespace Csis.Admission.IntegrationTests.Application.DependentActiveReasons;

internal sealed class GetAllDependentActiveReasonsQueryTests : BaseTestFixture
{
    [SetUp]
    public async Task SetUp() {
        for ( var i = 0; i < 5; i++ ) {
            await CreateDependentActiveReasonAsync();
        }
    }

    [Test]
    public async Task Handle_WhenCalled_ShouldReturnAllDependentActiveReasons() {
        var query = new GetAllDependentActiveReasonsQuery();
        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Should().HaveCount(5);
        result.Should().AllSatisfy(x => {
            x.Should().BeOfType<DependentActiveReasonDto>();
            x.Id.Should().BePositive();
        });
    }
}

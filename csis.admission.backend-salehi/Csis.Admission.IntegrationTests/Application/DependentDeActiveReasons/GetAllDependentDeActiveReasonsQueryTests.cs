using Csis.Admission.Application.Features.DependentDeActiveReasons.Dtos;
using Csis.Admission.Application.Features.DependentDeActiveReasons.Queries;
using Csis.Paging;
using Csis.Utilities;

namespace Csis.Admission.IntegrationTests.Application.DependentDeActiveReasons;

internal sealed class GetAllDependentDeActiveReasonsQueryTests : BaseTestFixture
{
    [SetUp]
    public async Task SetUp() {
        for ( var i = 0; i < 10; i++ ) {
            await CreateDependentDeActiveReasonAsync();
        }
    }

    [Test]
    public async Task Handle_WhenCalled_ShouldReturnAllDependentDeActiveReasons() {
        var query = new GetAllDependentDeActiveReasonsQuery();
        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Should().HaveCount(10);
        result.Should().AllSatisfy(x => {
            x.Should().BeOfType<DependentDeActiveReasonDto>();
            x.Id.Should().BePositive();
        });
    }
}

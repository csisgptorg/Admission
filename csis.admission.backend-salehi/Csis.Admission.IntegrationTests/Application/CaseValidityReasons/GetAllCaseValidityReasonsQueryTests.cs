using Csis.Admission.Application.Features.CaseValidityReasons.Dtos;
using Csis.Admission.Application.Features.CaseValidityReasons.Queries;
using Csis.Paging;
using Csis.Utilities;

namespace Csis.Admission.IntegrationTests.Application.CaseValidityReasons;

internal sealed class GetAllCaseValidityReasonsQueryTests : BaseTestFixture
{
    [SetUp]
    public async Task SetUp() {
        for ( var i = 0; i < 11; i++ ) {
            await CreateCaseValidityReasonsAsync();
        }
    }

    [Test]
    public async Task Handle_WhenCalled_ShouldReturnAllCaseValidityReasons() {
        var query = new GetAllCaseValidityReasonsQuery();
        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Should().HaveCount(11);
        result.Should().AllSatisfy(x => {
            x.Should().BeOfType<CaseValidityReasonsDto>();
            x.Id.Should().BePositive();
        });
    }
}

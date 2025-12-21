using Csis.Admission.Application.Features.CaseBlockReasons.Dtos;
using Csis.Admission.Application.Features.CaseBlockReasons.Queries;
using Csis.Paging;
using Csis.Utilities;

namespace Csis.Admission.IntegrationTests.Application.CaseBlockReasons;

internal sealed class GetAllCaseBlockReasonsQueryTests : BaseTestFixture
{
    [SetUp]
    public async Task SetUp() {
        for ( var i = 0; i < 8; i++ ) {
            await CreateCaseBlockReasonAsync();
        }
    }

    [Test]
    public async Task Handle_WhenCalled_ShouldReturnAllCaseBlockReasons() {
        var query = new GetAllCaseBlockReasonsQuery();
        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Should().HaveCount(8);
        result.Should().AllSatisfy(x => {
            x.Should().BeOfType<CaseBlockReasonDto>();
            x.Id.Should().BePositive();
        });
    }
}

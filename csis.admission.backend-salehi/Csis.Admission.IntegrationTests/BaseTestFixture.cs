namespace Csis.Admission.IntegrationTests;

[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUp() {
        await ResetState();
    }
}

using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Features.Famous.Dtos;
using Csis.Admission.Application.Features.Famous.Queries;
using Csis.Admission.Domain.Entities;
using Csis.Utilities;

namespace Csis.Admission.IntegrationTests.Application.Famous;

internal sealed class GetFamousByIdQueryTests : BaseTestFixture
{
    private Famous _famous;

    [SetUp]
    public async Task SetUp() {
        _famous = await CreateFamousAsync();
    }

    [Test]
    public async Task Handle_WhenCalled_ShouldReturnFamous() {
        var famous = await SendAsync(new GetFamousByIdQuery(_famous.Id));

        famous.Should().NotBeNull();
        famous.Should().BeOfType<FamousDto>();
        famous.Id.Should().Be(_famous.Id);
        famous.ActionPlace.Should().Be(_famous.ActionPlace);
        famous.Position.Should().Be(_famous.Position);
        famous.Codm.Should().Be(_famous.Codm);
        famous.Area.Should().Be(_famous.Area);
        famous.Role.Should().Be(_famous.Role);
        famous.Type.Should().Be(_famous.Type);
    }

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(int.MaxValue)]
    public async Task HandleGetById_WhenFamousIdIsInvalid_ShouldThrowException(int id) {
        await FluentActions
            .Invoking(() => SendAsync(new GetFamousByIdQuery(id)))
            .Should()
            .ThrowAsync<RecordNotFoundException<Famous>>();
    }
}

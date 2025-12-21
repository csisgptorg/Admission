//using Csis.Abstractions.Exceptions;
//using Csis.Admission.Application.Features.Marriages.Dtos;
//using Csis.Admission.Application.Features.Marriages.Queries;
//using Csis.Admission.Domain.Entities;
//using Csis.Utilities;

//namespace Csis.Admission.IntegrationTests.Application.Marriages;

//internal sealed class GetMarriageByIdQueryTests : BaseTestFixture
//{
//    private Marriage _marriage;

//    [SetUp]
//    public async Task SetUp() {
//        _marriage = await CreateMarriageAsync();
//    }

//    [Test]
//    public async Task Handle_WhenCalled_ShouldReturnMarriage() {
//        var marriage = await SendAsync(new GetMarriageByIdQuery(_marriage.Id));

//        marriage.Should().NotBeNull();
//        marriage.Should().BeOfType<MarriageDto>();
//        marriage.Id.Should().Be(_marriage.Id);
//        marriage.HusbandPersonId.Should().Be(_marriage.HusbandPersonId);
//        marriage.WifePersonId.Should().Be(_marriage.WifePersonId);
//        marriage.DeathDate.Should().Be(_marriage.DeathDate);
//        marriage.DivorceDate.Should().Be(_marriage.DivorceDate);
//        marriage.MarriageDate.Should().Be(_marriage.MarriageDate);
//    }

//    [TestCase(-1)]
//    [TestCase(0)]
//    [TestCase(int.MaxValue)]
//    public async Task HandleGetById_WhenMarriageIdIsInvalid_ShouldThrowException(int id) {
//        await FluentActions
//            .Invoking(() => SendAsync(new GetMarriageByIdQuery(id)))
//            .Should()
//            .ThrowAsync<RecordNotFoundException<Marriage>>();
//    }
//}

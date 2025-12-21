//using Csis.Abstractions.Exceptions;
//using Csis.Admission.Application.Features.Marriages.Commands;
//using Csis.Admission.Domain.Entities;
//using Csis.Utilities;

//namespace Csis.Admission.IntegrationTests.Application.Marriages;

//internal sealed class DeleteMarriageCommandTests : BaseTestFixture
//{
//    [Test]
//    public async Task Handle_WhenCalled_ShouldDeleteMarriage() {
//        var marriageId = (await CreateMarriageAsync()).Id;

//        marriageId.Should().BePositive();

//        await SendAsync(new DeleteMarriageCommand(marriageId));

//        var marriage = await FindAsync<Marriage>(marriageId);
//        marriage.Should().BeNull();
//    }

//    [TestCase(-1)]
//    [TestCase(0)]
//    [TestCase(int.MaxValue)]
//    public async Task HandleDelete_WhenMarriageIdIsInvalid_ShouldThrowException(int id) {
//        await FluentActions
//            .Invoking(() => SendAsync(new DeleteMarriageCommand(id)))
//            .Should()
//            .ThrowAsync<RecordNotFoundException<Marriage>>();
//    }
//}

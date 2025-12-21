//using Csis.Admission.Application.Features.Marriages.Dtos;
//using Csis.Admission.Application.Features.Marriages.Queries;
//using Csis.Paging;
//using Csis.Utilities;

//namespace Csis.Admission.IntegrationTests.Application.Marriages;

//internal sealed class SearchMarriagesQueryTests : BaseTestFixture
//{
//    [SetUp]
//    public async Task SetUp() {
//        await CreateMarriageAsync();
//    }

//    [Test]
//    public async Task Handle_WhenCalled_ShouldReturnMarriagesPagedList() {
//        // Page size 5
//        var result = await SendAsync(new SearchMarriagesQuery {
//            PageIndex = 1,
//            PageSize = 5
//        });

//        result.Should().NotBeNull();
//        result.Should().BeOfType<PagedList<MarriageDto>>();
//        result.Should().HaveCount(5);
//        result.TotalCount.Should().Be(6);
//        result.TotalPages.Should().Be(2);
//        result.HasNextPage.Should().BeTrue();
//        result.HasPreviousPage.Should().BeFalse();

//        // Page size 10
//        result = await SendAsync(new SearchMarriagesQuery {
//            PageIndex = 1,
//            PageSize = 10
//        });

//        result.Should().NotBeNull();
//        result.Should().BeOfType<PagedList<MarriageDto>>();
//        result.Should().HaveCount(6);
//        result.TotalCount.Should().Be(6);
//        result.TotalPages.Should().Be(1);
//        result.HasNextPage.Should().BeFalse();
//        result.HasPreviousPage.Should().BeFalse();

//        // Page index 2
//        result = await SendAsync(new SearchMarriagesQuery {
//            PageIndex = 2,
//            PageSize = 5
//        });

//        result.Should().NotBeNull();
//        result.Should().BeOfType<PagedList<MarriageDto>>();
//        result.Should().HaveCount(1);
//        result.TotalCount.Should().Be(6);
//        result.TotalPages.Should().Be(2);
//        result.HasNextPage.Should().BeFalse();
//        result.HasPreviousPage.Should().BeTrue();
//    }
//}

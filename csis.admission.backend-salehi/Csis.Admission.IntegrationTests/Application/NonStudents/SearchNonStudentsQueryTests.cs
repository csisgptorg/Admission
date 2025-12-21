//using Csis.Admission.Application.Features.NonStudents.Dtos;
//using Csis.Admission.Application.Features.NonStudents.Queries;
//using Csis.Paging;
//using Csis.Utilities;

//namespace Csis.Admission.IntegrationTests.Application.NonStudents;

//internal sealed class SearchNonStudentsQueryTests : BaseTestFixture
//{
//    [SetUp]
//    public async Task SetUp() {
//        var person = await CreatePersonAsync(fidaCode: "jlopqgsf", nationalCode: "qitwibom", yektaCode: "boercnhm");
//        await CreateNonStudentAsync(personId: person.Id);
//    }

//    [Test]
//    public async Task Handle_WhenCalled_ShouldReturnNonStudentsPagedList() {
//        // Page size 5
//        var result = await SendAsync(new SearchNonStudentsQuery {
//            PageIndex = 1,
//            PageSize = 5
//        });

//        result.Should().NotBeNull();
//        result.Should().BeOfType<PagedList<NonStudentDto>>();
//        result.Should().HaveCount(5);
//        result.TotalCount.Should().Be(6);
//        result.TotalPages.Should().Be(2);
//        result.HasNextPage.Should().BeTrue();
//        result.HasPreviousPage.Should().BeFalse();

//        // Page size 10
//        result = await SendAsync(new SearchNonStudentsQuery {
//            PageIndex = 1,
//            PageSize = 10
//        });

//        result.Should().NotBeNull();
//        result.Should().BeOfType<PagedList<NonStudentDto>>();
//        result.Should().HaveCount(6);
//        result.TotalCount.Should().Be(6);
//        result.TotalPages.Should().Be(1);
//        result.HasNextPage.Should().BeFalse();
//        result.HasPreviousPage.Should().BeFalse();

//        // Page index 2
//        result = await SendAsync(new SearchNonStudentsQuery {
//            PageIndex = 2,
//            PageSize = 5
//        });

//        result.Should().NotBeNull();
//        result.Should().BeOfType<PagedList<NonStudentDto>>();
//        result.Should().HaveCount(1);
//        result.TotalCount.Should().Be(6);
//        result.TotalPages.Should().Be(2);
//        result.HasNextPage.Should().BeFalse();
//        result.HasPreviousPage.Should().BeTrue();
//    }
//}

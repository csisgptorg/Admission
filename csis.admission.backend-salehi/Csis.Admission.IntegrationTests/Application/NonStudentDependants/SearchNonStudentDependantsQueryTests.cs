//using Csis.Admission.Application.Features.NonStudentDependants.Dtos;
//using Csis.Admission.Application.Features.NonStudentDependants.Queries;
//using Csis.Paging;
//using Csis.Utilities;

//namespace Csis.Admission.IntegrationTests.Application.NonStudentDependants;

//internal sealed class SearchNonStudentDependantsQueryTests : BaseTestFixture
//{
//    [SetUp]
//    public async Task SetUp() {
//        var person = await CreatePersonAsync(fidaCode: "fpriisii", nationalCode: "acxnozyl", yektaCode: "uovggsqd");
//        await CreateNonStudentDependantAsync(personId: person.Id, nonStudentCodm: 820682, isActive: true);
//        await CreateNonStudentDependantAsync(personId: person.Id, nonStudentCodm: 820682, isActive: true);
//        await CreateNonStudentDependantAsync(personId: person.Id, nonStudentCodm: 820682, isActive: true);
//        await CreateNonStudentDependantAsync(personId: person.Id, nonStudentCodm: 820682, isActive: true);
//        await CreateNonStudentDependantAsync(personId: person.Id, nonStudentCodm: 820682, isActive: true);
//        await CreateNonStudentDependantAsync(personId: person.Id, nonStudentCodm: 820682, isActive: true);
//    }

//    [Test]
//    public async Task Handle_WhenCalled_ShouldReturnNonStudentDependantsPagedList() {
//        // Page size 5
//        var result = await SendAsync(new SearchNonStudentDependantsQuery {
//            PageIndex = 1,
//            PageSize = 5
//        });

//        result.Should().NotBeNull();
//        result.Should().BeOfType<PagedList<NonStudentDependantDto>>();
//        result.Should().HaveCount(5);
//        result.TotalCount.Should().Be(6);
//        result.TotalPages.Should().Be(2);
//        result.HasNextPage.Should().BeTrue();
//        result.HasPreviousPage.Should().BeFalse();

//        // Page size 10
//        result = await SendAsync(new SearchNonStudentDependantsQuery {
//            PageIndex = 1,
//            PageSize = 10
//        });

//        result.Should().NotBeNull();
//        result.Should().BeOfType<PagedList<NonStudentDependantDto>>();
//        result.Should().HaveCount(6);
//        result.TotalCount.Should().Be(6);
//        result.TotalPages.Should().Be(1);
//        result.HasNextPage.Should().BeFalse();
//        result.HasPreviousPage.Should().BeFalse();

//        // Page index 2
//        result = await SendAsync(new SearchNonStudentDependantsQuery {
//            PageIndex = 2,
//            PageSize = 5
//        });

//        result.Should().NotBeNull();
//        result.Should().BeOfType<PagedList<NonStudentDependantDto>>();
//        result.Should().HaveCount(1);
//        result.TotalCount.Should().Be(6);
//        result.TotalPages.Should().Be(2);
//        result.HasNextPage.Should().BeFalse();
//        result.HasPreviousPage.Should().BeTrue();
//    }
//}

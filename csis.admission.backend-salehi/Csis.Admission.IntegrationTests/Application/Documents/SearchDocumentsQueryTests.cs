//using Csis.Admission.Application.Features.Documents.Dtos;
//using Csis.Admission.Application.Features.Documents.Queries;
//using Csis.Paging;
//using Csis.Utilities;

//namespace Csis.Admission.IntegrationTests.Application.Documents;

//internal sealed class SearchDocumentsQueryTests : BaseTestFixture
//{
//    [SetUp]
//    public async Task SetUp() {
//        var person = await CreatePersonAsync(fidaCode: "jnvmmdjc", nationalCode: "hebwvclg", yektaCode: "rrmqeztb");
//        await CreateDocumentAsync(fileIdentifier: StringHelper.Random(6), personId: person.Id);
//        await CreateDocumentAsync(fileIdentifier: StringHelper.Random(6), personId: person.Id);
//        await CreateDocumentAsync(fileIdentifier: StringHelper.Random(6), personId: person.Id);
//        await CreateDocumentAsync(fileIdentifier: StringHelper.Random(6), personId: person.Id);
//        await CreateDocumentAsync(fileIdentifier: StringHelper.Random(6), personId: person.Id);
//        await CreateDocumentAsync(fileIdentifier: StringHelper.Random(6), personId: person.Id);
//    }

//    [Test]
//    public async Task Handle_WhenCalled_ShouldReturnDocumentsPagedList() {
//        // Page size 5
//        var result = await SendAsync(new SearchDocumentsQuery {
//            PageIndex = 1,
//            PageSize = 5
//        });

//        result.Should().NotBeNull();
//        result.Should().BeOfType<PagedList<DocumentDto>>();
//        result.Should().HaveCount(5);
//        result.TotalCount.Should().Be(6);
//        result.TotalPages.Should().Be(2);
//        result.HasNextPage.Should().BeTrue();
//        result.HasPreviousPage.Should().BeFalse();

//        // Page size 10
//        result = await SendAsync(new SearchDocumentsQuery {
//            PageIndex = 1,
//            PageSize = 10
//        });

//        result.Should().NotBeNull();
//        result.Should().BeOfType<PagedList<DocumentDto>>();
//        result.Should().HaveCount(6);
//        result.TotalCount.Should().Be(6);
//        result.TotalPages.Should().Be(1);
//        result.HasNextPage.Should().BeFalse();
//        result.HasPreviousPage.Should().BeFalse();

//        // Page index 2
//        result = await SendAsync(new SearchDocumentsQuery {
//            PageIndex = 2,
//            PageSize = 5
//        });

//        result.Should().NotBeNull();
//        result.Should().BeOfType<PagedList<DocumentDto>>();
//        result.Should().HaveCount(1);
//        result.TotalCount.Should().Be(6);
//        result.TotalPages.Should().Be(2);
//        result.HasNextPage.Should().BeFalse();
//        result.HasPreviousPage.Should().BeTrue();
//    }
//}

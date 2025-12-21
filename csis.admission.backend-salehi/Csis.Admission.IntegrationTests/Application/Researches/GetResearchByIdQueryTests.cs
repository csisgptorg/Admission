using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Features.Researches.Dtos;
using Csis.Admission.Application.Features.Researches.Queries;
using Csis.Admission.Domain.Entities;
using Csis.Utilities;

namespace Csis.Admission.IntegrationTests.Application.Researches;

internal sealed class GetResearchByIdQueryTests : BaseTestFixture
{
    private Research _research;

    [SetUp]
    public async Task SetUp() {
        _research = await CreateResearchAsync();
    }

    [Test]
    public async Task Handle_WhenCalled_ShouldReturnResearch() {
        var research = await SendAsync(new GetResearchByIdQuery(_research.Id));

        research.Should().NotBeNull();
        research.Should().BeOfType<ResearchDto>();
        research.Id.Should().Be(_research.Id);
        research.ArticlePublication.Should().Be(_research.ArticlePublication);
        research.BookPublisher.Should().Be(_research.BookPublisher);
        research.BookShabak.Should().Be(_research.BookShabak);
        research.ProjectEmployer.Should().Be(_research.ProjectEmployer);
        research.Title.Should().Be(_research.Title);
        research.SubjectId.Should().Be(_research.SubjectId);
        research.Year.Should().Be(_research.Year);
        research.AuditPersonId.Should().Be(_research.AuditPersonId);
        research.AuditRequestId.Should().Be(_research.AuditRequestId);
        research.Codm.Should().Be(_research.Codm);
        research.AuditDataSource.Should().Be(_research.AuditDataSource);
        research.Language.Should().Be(_research.Language);
        research.Subject.Should().Be(_research.Subject);
        research.TempId.Should().Be(_research.TempId);
        research.Type.Should().Be(_research.Type);
    }

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(int.MaxValue)]
    public async Task HandleGetById_WhenResearchIdIsInvalid_ShouldThrowException(int id) {
        await FluentActions
            .Invoking(() => SendAsync(new GetResearchByIdQuery(id)))
            .Should()
            .ThrowAsync<RecordNotFoundException<Research>>();
    }
}

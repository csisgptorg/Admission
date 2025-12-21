using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Features.Researches.Commands;
using Csis.Admission.Application.Features.Researches.Validators;
using Csis.Admission.Domain.Entities;
using Csis.Utilities;
using FluentValidation.TestHelper;

namespace Csis.Admission.IntegrationTests.Application.Researches;

internal sealed class UpdateResearchCommandTests : BaseTestFixture
{
    [Test]
    public async Task Handle_WhenCalled_ShouldUpdateResearch() {
        var researchId = (await CreateResearchAsync()).Id;

        researchId.Should().BePositive();

        var command = new UpdateResearchCommand {
            Id = researchId,
            ArticlePublication = "ivceppul",
            BookPublisher = "tdczapta",
            BookShabak = "wsyuqzuy",
            ProjectEmployer = "qsjhzucs",
            Title = "repellendus",
            SubjectId = 5184,
            Year = 8688,
            AuditPersonId = 88915,
            AuditRequestId = 52030,
            Codm = 28155,
            AuditDataSource = new DataSource?(),
            Language = new ResearchLanguage?(),
            Subject = new ResearchSubject(),
            TempId = new Guid?(),
            Type = new ResearchType?(),
        };

        await SendAsync(command);

        var research = await FindAsync<Research>(researchId);

        research.Id.Should().Be(researchId);
        research.ArticlePublication.Should().Be("ivceppul");
        research.BookPublisher.Should().Be("tdczapta");
        research.BookShabak.Should().Be("wsyuqzuy");
        research.ProjectEmployer.Should().Be("qsjhzucs");
        research.Title.Should().Be("repellendus");
        research.SubjectId.Should().Be(5184);
        research.Year.Should().Be(8688);
        research.AuditPersonId.Should().Be(88915);
        research.AuditRequestId.Should().Be(52030);
        research.Codm.Should().Be(28155);
        research.AuditDataSource.Should().Be(new DataSource?());
        research.Language.Should().Be(new ResearchLanguage?());
        research.Subject.Should().Be(new ResearchSubject());
        research.TempId.Should().Be(new Guid?());
        research.Type.Should().Be(new ResearchType?());
        research.UpdatedOn.Should().NotBeNull();
        research.LastUpdatedById.Should().NotBeNull();
        research.DeletedOn.Should().Be(null);
        research.DeletedById.Should().Be(null);
        research.Deleted.Should().BeFalse();
    }

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(int.MaxValue)]
    public async Task HandleUpdate_WhenResearchIdIsInvalid_ShouldThrowException(int id) {
        await FluentActions
            .Invoking(() => SendAsync(new UpdateResearchCommand { Id = id }))
            .Should()
            .ThrowAsync<RecordNotFoundException<Research>>();
    }

    [Test]
    public async Task Handle_WhenUpdateResearchCommandInputIsInvalid_ShouldHaveValidationError() {
        var command = new UpdateResearchCommand();
        var validator = new UpdateResearchCommandValidator();

        var result = await validator.TestValidateAsync(command);

    }
}

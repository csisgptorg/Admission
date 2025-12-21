using Csis.Admission.Application.Features.Researches.Commands;
using Csis.Admission.Application.Features.Researches.Validators;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Domain.Enums;
using Csis.Utilities;
using FluentValidation.TestHelper;

namespace Csis.Admission.IntegrationTests.Application.Researches;

internal sealed class CreateResearchCommandTests : BaseTestFixture
{
    [Test]
    public async Task Handle_WhenCalled_ShouldCreateResearch() {
        var command = new CreateResearchCommand {
            ArticlePublication = "xkxkztnz",
            BookPublisher = "tcpbwpad",
            BookShabak = "swwqmvbt",
            ProjectEmployer = "gargyqao",
            Title = "quod",
            SubjectId = 4419,
            Year = 71,
            Codm = 49680,
            Language = new ResearchLanguage?(),
            Type = new ResearchType?(),
        };

        var id = await SendAsync(command);

        var research = await FindAsync<Research>(id);

        research.ArticlePublication.Should().Be("xkxkztnz");
        research.BookPublisher.Should().Be("tcpbwpad");
        research.BookShabak.Should().Be("swwqmvbt");
        research.ProjectEmployer.Should().Be("gargyqao");
        research.Title.Should().Be("quod");
        research.SubjectId.Should().Be(4419);
        research.Year.Should().Be(71);
        research.AuditPersonId.Should().Be(1898);
        research.AuditRequestId.Should().Be(97275);
        research.Codm.Should().Be(49680);
        research.Language.Should().Be(new ResearchLanguage?());
        research.Subject.Should().Be(new ResearchSubject());
        research.TempId.Should().Be(new Guid?());
        research.Type.Should().Be(new ResearchType?());
        research.UpdatedOn.Should().Be(null);
        research.LastUpdatedById.Should().Be(null);
        research.DeletedOn.Should().Be(null);
        research.DeletedById.Should().Be(null);
        research.Deleted.Should().BeFalse();
    }

    [Test]
    public async Task Handle_WhenCreateResearchCommandInputIsInvalid_ShouldHaveValidationError() {
        var command = new CreateResearchCommand();
        var validator = new CreateResearchCommandValidator();

        var result = await validator.TestValidateAsync(command);

    }
}

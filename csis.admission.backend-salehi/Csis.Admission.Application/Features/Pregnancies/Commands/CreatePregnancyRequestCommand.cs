using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Pregnancies.Commands;

/// <inheritdoc/>
public sealed record CreatePregnancyRequestCommand : BaseCommandDto<CreatePregnancyRequestCommand, Pregnancy>, IRequest
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public string StartDate { get; init; }

    /// <inheritdoc/>
    public string EndDate { get; init; }

    /// <inheritdoc/>
    public Guid FileId { get; init; }

    /// <inheritdoc/>
    public override void ReverseCustomMappings(IMappingExpression<CreatePregnancyRequestCommand, Pregnancy> mapping) {
        mapping.ForMember(model => model.StartDate, config => config.MapFrom(dto => dto.StartDate.StringDateToInt()));
        mapping.ForMember(model => model.EndDate, config => config.MapFrom(dto => dto.EndDate.StringDateToInt()));
    }
}

internal sealed class CreatePregnancyRequestCommandHandler(IRequestService requestService, IRepository<Pregnancy> repo, IStudentDataService studentDataService, IRepository<UploadedFile> uploadRepo,
    IStudentRepository studentRepository) : IRequestHandler<CreatePregnancyRequestCommand>
{
    public async Task Handle(CreatePregnancyRequestCommand request, CancellationToken cancellationToken) {

        _ = await studentRepository.GetCaseByCodm(request.Codm);

        var requestCommand = new CreateRequestCommand(request, RequestFlow.StudentToEmployee);
        requestCommand.AddDocument(request.FileId);
        await requestService.Create(requestCommand, cancellationToken);
    }
}

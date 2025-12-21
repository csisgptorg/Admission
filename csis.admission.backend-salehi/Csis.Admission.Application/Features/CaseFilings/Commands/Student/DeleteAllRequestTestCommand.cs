using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Features.CaseFilings.Dtos;
using Csis.Admission.Domain.Entities;
using System.Text.Json;

namespace Csis.Admission.Application.Features.CaseFilings.Commands;

public sealed record DeleteAllRequestTestCommand : IRequest;

internal sealed class DeleteAllRequestTestCommandHandler(ICaseFillingRequestRepository repository) : IRequestHandler<DeleteAllRequestTestCommand>
{
    public async Task Handle(DeleteAllRequestTestCommand request, CancellationToken cancellationToken) {
        await repository.DeleteAll();

    }
}

using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Features.Auth.Commands;
using Csis.Admission.Domain.Enums;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading;

namespace Csis.Admission.Services;

/// <inheritdoc/>
internal sealed partial class CaseFillingRequestService : ICaseFillingRequestService
{
    private async Task SendCommand(Domain.Entities.CaseFillingRequest request, CancellationToken cancellation) {
        if ( request.ApprovalStatus == ApprovalStatus.Approved && request.NextFlowApprover == RequestApprovalFlow.TheEnd ) {
            try {
                var command = PayloadToCommand(request);
                var result = (await mediator.Send(command, cancellation))?.ToString();
                if ( !long.TryParse(result, out var id) ) {
                    id = -1;
                }

                request.RecordId = id;
            } catch ( CommandValidationException exception ) {
                logger.LogError(exception, "Error in RequestService.SendCommand for Type: {type}", request.Type);
                request.RecordId = null;
                request.Description = exception.ToString();
                await repo.DeleteAsync(request, true, cancellation);
                throw new CommandValidationException(exception.Message);
            }
        }
    }

    private static readonly JsonSerializerOptions _serializerOptions = new() { PropertyNameCaseInsensitive = true };
    private static object PayloadToCommand(Domain.Entities.CaseFillingRequest request) {
        var application = typeof(LoginCommand).Assembly;
        var command = request.PayloadModel.Replace("Request", "");
        var type = application.GetTypes().SingleOrDefault(x => x.Name == command)
            ?? throw new BadRequestException($"PayloadModel not found: {request.PayloadModel}");

        var instance = JsonSerializer.Deserialize(request.Payload, type, _serializerOptions);
        var property = instance.GetType().GetProperty("RequestId");
        property?.SetValue(instance, request.Id);

        return instance;
    }
}

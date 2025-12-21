using System.Text.Json;
using Csis.Admission.Domain.Enums;
using Microsoft.Extensions.Logging;
using Csis.Abstractions.Exceptions;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Features.Auth.Commands;

namespace Csis.Admission.Services;

/// <inheritdoc/>
internal sealed partial class RequestService : IRequestService
{
    private async Task SendCommand(Request request, CancellationToken cancellation) {
        if ( request.ApprovalStatus == ApprovalStatus.Approved && request.NextFlowApprover == RequestApprovalFlow.TheEnd ) {
            try {
                var command = PayloadToCommand(request);
                var commandName = command.GetType().Name;
                var result = await mediator.Send(command, cancellation);
                if ( result is System.Collections.IEnumerable items) {
                    request.RecordIds = items.Cast<object>().Select(Convert.ToInt64).ToList();

                } else if ( long.TryParse(result?.ToString(), out var id) ) {
                    request.RecordId = id;

                } else {
                    request.RecordId = -1;
                }

            } catch ( Exception exception ) {
                logger.LogError(exception, "Error in RequestService.SendCommand for Type: {type}", request.Type);
                var traceLog = new TraceLog("", "RequestService > SendCommand",exception.ToString(), "ResponseException");
                await logRepo.InsertAsync(traceLog,true,cancellation);
                request.RecordId = -1;
            }
        }
    }

    private static readonly JsonSerializerOptions _serializerOptions = new() { PropertyNameCaseInsensitive = true };
    private static object PayloadToCommand(Request request) {
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

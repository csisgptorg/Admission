using Csis.Admission.Application.Common.Interfaces.Repositories;

namespace Csis.Admission.Application.Features.People.Commands;

/// <summary>
/// بروزرسان? تصو?ر شخص
/// </summary>
public sealed record UpdatePersonImageCommand : IRequest
{
    /// <summary>
    /// شناسه شخص
    /// </summary>
    public int PersonId { get; init; }

    /// <summary>
    /// شناسه تصویر شخص
    /// </summary>
    public Guid? PersonImage { get; init; }
}

internal sealed class UpdatePersonImageCommandHandler(
    IPersonRepository personRepo,
    ILogger<UpdatePersonImageCommandHandler> logger)
    : IRequestHandler<UpdatePersonImageCommand>
{
    public async Task Handle(UpdatePersonImageCommand request, CancellationToken cancellationToken)
    {
        logger.LogDebug("Updating person image for person with id {personId}", request.PersonId);

        var person = await personRepo.GetByIdAsTrackingAsync(request.PersonId, cancellationToken: cancellationToken)
            ?? throw new CommandValidationException( $"Person with id {request.PersonId} not found.");

        logger.LogDebug("Person {personId} image before update: {before}", request.PersonId, person.PersonImage);

        person.PersonImage = request.PersonImage;

        logger.LogDebug("Person {personId} image after update: {after}", request.PersonId, person.PersonImage);

        await personRepo.UpdateAsync(person, cancellationToken: cancellationToken);
        
        logger.LogDebug("Person image updated successfully for person with id {personId}", request.PersonId);
    }
}

namespace Csis.Admission.Application.Common.Dtos;

/// <inheritdoc/>
public class ProcedureResultDto
{
    /// <inheritdoc/>
    public long Id { get; set; }

    /// <inheritdoc/>
    public bool IsSuccess { get; set; }

    /// <inheritdoc/>
    public string Message { get; set; }

    /// <inheritdoc/>
    public void ThrowIfUnsuccessful() {
        if ( IsSuccess == false ) {
            throw new CommandValidationException(Message);
        }
    }
}

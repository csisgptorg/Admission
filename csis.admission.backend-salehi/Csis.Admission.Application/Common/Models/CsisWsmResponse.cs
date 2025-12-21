namespace Csis.Admission.Application.Common.Models;

/// <inheritdoc/>
public sealed class CsisWsmResponse<TResponse>
{
    /// <inheritdoc/>
    public TResponse Response { get; set; }

    /// <inheritdoc/>
    public ExtraModel Extra { get; set; }

    /// <inheritdoc/>
}

public sealed class CsisWsmApiResponse<TResponse>
{
    public TResponse Response { get; set; }
    public TResponse Data { get; set; }
    public ExtraModel Extra { get; set; }

    /// <summary> </summary>
    public bool IsValid() => Extra.IsSuccess;
}
public record ExtraModel(string RequestId, string ExceptionMessage, DateTime Date, int StatusCode, bool IsSuccess);


public record Error(List<string> Errors, object ErrorDetails, string Message, bool IsSuccess, int StatusCode, bool IsExceptionThrown);

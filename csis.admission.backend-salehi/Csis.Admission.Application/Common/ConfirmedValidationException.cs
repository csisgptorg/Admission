namespace Csis.Admission.Application.Common;

/// <inheritdoc/>
public sealed class ConfirmedValidationException : Exception
{
    /// <inheritdoc/>
    public ConfirmedValidationException(object data=null) {
        Data = data;
    }

    /// <summary>دیتا</summary>
    public new object Data { get; set; }
}

namespace Csis.Admission.Application.Extensions;

///<inheritdoc/>
public static class GuidExtensions
{
    ///<inheritdoc/>
    public static bool IsEmpty(this Guid input) {
        return input == Guid.Empty;
    }

    /// <inheritdoc/>
    public static bool IsEmpty(this Guid? input) {
        return !input.HasValue || input == Guid.Empty;
    }
}

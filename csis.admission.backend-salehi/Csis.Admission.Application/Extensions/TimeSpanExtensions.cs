namespace Csis.Admission.Application.Extensions;

/// <summary>
/// TimeSpan Extensions
/// </summary>
public static class TimeSpanExtensions
{
    /// <summary>
    /// TimeToString
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string TimeToString(this TimeSpan input) {
        return input.ToString(@"hh\:mm");
    }
}

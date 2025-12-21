using System.Diagnostics.CodeAnalysis;

namespace Csis.Admission.Application.Extensions;

/// <inheritdoc/>
public static class StringExtensions
{
    /// <inheritdoc/>
    public static int? StringDateToInt(this string input) {

        if ( !string.IsNullOrWhiteSpace(input) )
            input = input.Replace("/", "").Replace("-", "");

        if ( int.TryParse(input, out var result) ) {
            return result;
        }

        return default;
    }

    /// <inheritdoc/>
    public static TimeSpan StringTimeToTimeSpan(this string input) {
        if ( TimeSpan.TryParse(input, out var result) ) {
            return result;
        }
        return default;
    }

    /// <inheritdoc/>
    public static bool EqualIgnoreCase(this string input,string value) {
        return input.Equals(value, StringComparison.OrdinalIgnoreCase);
    }
    
}

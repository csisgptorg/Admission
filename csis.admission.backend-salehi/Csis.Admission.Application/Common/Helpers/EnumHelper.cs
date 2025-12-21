using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace Csis.Admission.Application.Common.Helpers;
/// <inheritdoc/>
public static class EnumHelper
{
    /// <inheritdoc/>
    public static Dictionary<int, string> GetEnumKeyValuePairs(Type enumType) {
        return Enum.GetValues(enumType).Cast<Enum>()
                   .ToDictionary(x => (int) Convert.ChangeType(x, typeof(int)), GetDisplayName);
    }

    private static string GetDisplayName(Enum value) {
        var field = value.GetType().GetField(value.ToString());
        var displayAttribute = field?.GetCustomAttribute<DisplayAttribute>();

        return displayAttribute?.Name ?? value.ToString();
    }
}

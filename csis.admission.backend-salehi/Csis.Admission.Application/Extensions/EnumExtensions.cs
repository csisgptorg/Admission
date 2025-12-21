/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Extensions;

/// <summary>
/// Enum extension methods
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the attribute of the enum value
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute {
        var enumType = value.GetType();
        var name = Enum.GetName(enumType, value);
        return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
    }
}

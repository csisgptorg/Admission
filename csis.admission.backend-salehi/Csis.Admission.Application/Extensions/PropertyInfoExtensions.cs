using System.Reflection;
using System.ComponentModel;

namespace Csis.Admission.Application.Extensions;

/// <inheritdoc/>
public static class PropertyInfoExtensions
{
    /// <summary>دریافت نام نمایشی</summary>
    public static string GetDisplayName(this PropertyInfo prop) {
        var displayNameAttr = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false)
                                  .FirstOrDefault() as DisplayNameAttribute;

        return displayNameAttr is not null ? displayNameAttr.DisplayName : prop.Name;
    }

    /// <summary>اینام است</summary>
    public static bool IsEnum(this PropertyInfo prop) {
        return prop.PropertyType.IsEnum || Nullable.GetUnderlyingType(prop.PropertyType)?.IsEnum == true;
    }

    /// <summary>دریافت نوع پایه</summary>
    public static Type GetUnderlyingType(this PropertyInfo prop) {
        return Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
    }
}

using Csis.Authorization;
using Csis.Admission.Application.Enums;

namespace Csis.Admission.Application.Extensions;

/// <inheritdoc/>
public static class PermissionExtensions
{
    /// <inheritdoc/>
    public static string GetDescription(this PermissionsEnum permission) {
        var field = permission.GetType().GetField(permission.ToString());
        if ( field != null ) {
            var attribute = (PermissionAttribute) Attribute.GetCustomAttribute(field, typeof(PermissionAttribute));
            if ( attribute != null ) {
                return attribute.Description;
            }
        }
        return null;
    }
}

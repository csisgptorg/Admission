/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Utilities.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Csis.Admission.WebApi.Filters;

/// <summary>
/// نمایش دسترسی مورد نیاز جهت اجرای اکشن
/// </summary>
internal sealed class SwaggerActionPermission : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context) {
        var actionAttributes = context.MethodInfo.GetCustomAttributes<CsisAuthorizeAttribute>(true);
        var controllerAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes<CsisAuthorizeAttribute>(true);
        var allAttributes = controllerAttributes.Concat(actionAttributes).ToArray();

        if ( allAttributes.Length > 0 ) {

            operation.Description += "<div>Required permissions: ";
            var permissions = new List<string>();

            foreach ( var attribute in allAttributes ) {
                var permissionCodes = attribute.GetPermissionCodes();

                if ( permissionCodes.Count > 0 ) {
                    permissions.Add($"({string.Join(',', permissionCodes)} -> {attribute.Mode})");
                } else {
                    permissions.Add("Login");
                }
            }

            operation.Description += $"{string.Join(", ", permissions)}</div>";
        } else {
            var studentActionAttributes = context.MethodInfo.GetCustomAttributes<CsisAuthorizeStudentAttribute>(true);
            var studentControllerAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes<CsisAuthorizeStudentAttribute>(true);
            var studentAllAttributes = studentControllerAttributes.Concat(studentActionAttributes).ToArray();

            if ( studentAllAttributes.Length != 0 ) {

                if ( operation.Description.HasValue() ) {
                    operation.Description += "<br />";
                }

                operation.Description += "<div>Requires student login</div>";
            } else {
                operation.Description += "<div>Required permissions: UNPROTECTED</div>";
            }
        }
    }
}

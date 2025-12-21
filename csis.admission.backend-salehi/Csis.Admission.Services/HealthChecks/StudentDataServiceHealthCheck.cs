/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Interfaces;
using Csis.Utilities.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Csis.Admission.Services.HealthChecks;
internal sealed class StudentDataServiceHealthCheck(IStudentDataService studentDataService) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default) {
        try {
            var student = await studentDataService.GetStudentInfoAsync("82000")
                ?? throw new Exception("Could not get student info with codm 82000");

            if ( !student.Codm.Equals("82000") || !student.FirstName.HasValue() ) {
                throw new Exception("Incomplete data received from student data service");
            }

            return HealthCheckResult.Healthy("Student data service works as expected");

        } catch ( Exception ex ) {
            return new HealthCheckResult(
                context.Registration.FailureStatus,
                "Student data service is not working as expected",
                ex,
                new Dictionary<string, object> {
                    { "codm", "82000" }
                });
        }
    }
}

/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Microsoft.Extensions.Configuration;

namespace Csis.Admission.Persistence.DataSeeders;
internal interface IDataSeeder
{
    /// <summary>
    /// Seed the data
    /// </summary>
    /// <param name="serviceProvider">Service provider to get required services</param>
    /// <param name="configuration">Configurations</param>
    /// <returns></returns>
    Task SeedAsync(IServiceProvider serviceProvider, IConfiguration configuration);
}

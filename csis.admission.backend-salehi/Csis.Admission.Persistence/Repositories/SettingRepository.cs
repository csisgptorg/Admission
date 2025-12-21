/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Persistence.Repositories;

internal sealed class SettingRepository(AppDbContext dbContext) : ISettingRepository
{
    public async Task<Setting> GetByKeyAsTrackingAsync(string key) {
        return await dbContext.Set<Setting>().AsTracking().SingleOrDefaultAsync(x => x.Key == key);
    }

    public async Task<Setting> GetByKeyAsync(string key) {
        return await dbContext.Set<Setting>().AsNoTracking().SingleOrDefaultAsync(x => x.Key == key);
    }

    public async Task InsertAsync(Setting setting) {
        await dbContext.Set<Setting>().AddAsync(setting);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Setting setting) {
        dbContext.Set<Setting>().Update(setting);
        await dbContext.SaveChangesAsync();
    }
}

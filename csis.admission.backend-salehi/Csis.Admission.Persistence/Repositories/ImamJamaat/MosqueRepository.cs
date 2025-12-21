using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Interfaces.Repositories.ImamJamaat;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Csis.Admission.Persistence.Repositories.ImamJamaat;
internal class MosqueRepository : Repository<Mosque>, IMosqueRepository
{
    public MosqueRepository(AppDbContext dbContext, IMemoryCacheService cache, ICacheKeyService<int> cacheKeyService, IOptions<CacheOptions> cacheOptions, ICurrentUserService currentUserService) : base(dbContext, cache, cacheKeyService, cacheOptions, currentUserService) {
    }


    public async Task<Mosque> GetMosqueFullInfoAsync(int mosqueId, CancellationToken cancellationToken = default) {
        var dbSet = _dbContext.Set<Mosque>();
        return await dbSet.AsTracking().Where(x => x.Id == mosqueId)
            .Include(x => x.Imams)
            .ThenInclude(x => x.ActiveSpousesInMosque)
            .Include(x => x.MosqueActivity)
            .Include(x => x.MosqueAddress)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}

/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Persistence.Repositories;

internal sealed partial class FileRepository(
    AppDbContext dbContext,
    IMemoryCacheService cache,
    ICacheKeyService<int> cacheKeyService,
    IOptions<CacheOptions> cacheOptions,
    ICurrentUserService currentUserService) : Repository<UploadedFile>(dbContext, cache, cacheKeyService, cacheOptions, currentUserService), IFileRepository
{
    public async Task<bool> IsValidAsync(Guid identifier, FileTypes type, CancellationToken cancellationToken = default) {
        return await ExistsAsync(x => x.FileIdentifier == identifier && x.Type == type, cancellationToken: cancellationToken);
    }
}

using Microsoft.Extensions.Options;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Common.Interfaces.Repositories;

namespace Csis.Admission.Persistence.Repositories;

internal sealed class NonStudentRepository : Repository<NonStudent, long>, INonStudentRepository
{
    public NonStudentRepository(
        AppDbContext dbContext,
        IMemoryCacheService cache,
        ICacheKeyService<long> cacheKeyService,
        IOptions<CacheOptions> cacheOptions,
        ICurrentUserService currentUserService) : base(dbContext, cache, cacheKeyService, cacheOptions, currentUserService) {
    }
}

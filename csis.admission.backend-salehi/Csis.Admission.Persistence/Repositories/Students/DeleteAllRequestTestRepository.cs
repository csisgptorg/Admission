using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Csis.Admission.Persistence.Repositories.Students;

internal sealed class DeleteAllRequestTestRepository( AppDbContext dbContext, IMemoryCacheService cache, ICacheKeyService<long> cacheKeyService, IOptions<CacheOptions> cacheOptions, ICurrentUserService currentUserService) : Repository<CaseFillingRequest,long>(dbContext, cache, cacheKeyService, cacheOptions, currentUserService), ICaseFillingRequestRepository
{
    public async Task DeleteAll() {
        var deleted = await dbContext.Set<CaseFillingRequest>().ExecuteDeleteAsync();
    }
}

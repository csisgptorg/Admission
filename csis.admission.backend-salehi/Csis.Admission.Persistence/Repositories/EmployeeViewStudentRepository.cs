using Csis.Authorization.Services;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using AutoMapper.QueryableExtensions;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Features.ViewLogs.Dtos;
using Csis.Admission.Application.Common.Interfaces.Repositories;

namespace Csis.Admission.Persistence.Repositories;
internal sealed class EmployeeViewStudentLogRepository : Repository<EmployeeViewStudentLog,long>, IEmployeeViewStudentLogRepository
{
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;
    public EmployeeViewStudentLogRepository(AppDbContext dbContext, IMemoryCacheService cache, ICacheKeyService<long> cacheKeyService,
        IOptions<CacheOptions> cacheOptions, ICurrentUserService currentUserService, ICsisAuthenticatedUserService authenticatedUserService)
        : base(dbContext, cache, cacheKeyService, cacheOptions, currentUserService) {
        _authenticatedUserService = authenticatedUserService;
    }

    public async Task<EmployeeViewStudentLogDto[]> GetLastLogs(CancellationToken cancellationToken) {
        var personnelId = await _authenticatedUserService.GetPersonnelIdAsync();
        return await _dbContext.Set<EmployeeViewStudentLog>().Where(x => x.PersonnelId == personnelId)
            .OrderByDescending(x=>x.Id).Take(10).ProjectTo<EmployeeViewStudentLogDto>(_mappingProvider).ToArrayAsync(cancellationToken);
    }
}

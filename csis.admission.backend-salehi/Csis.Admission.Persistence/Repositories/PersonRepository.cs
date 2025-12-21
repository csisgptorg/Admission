using Microsoft.Extensions.Options;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Enums;
using Csis.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Csis.Admission.Persistence.Repositories;

internal sealed class PersonRepository(
    AppDbContext dbContext,
    IMemoryCacheService cache,
    ICacheKeyService<int> cacheKeyService,
    IOptions<CacheOptions> cacheOptions,
    ICurrentUserService currentUserService,
    AppDapperContext dapper)
    : Repository<Person>(dbContext, cache, cacheKeyService, cacheOptions, currentUserService), IPersonRepository
{
    public async Task<int> GetNextYektaCodeAsync() {
        var result = await dapper.ExecuteProcedureSingleOrDefault<int>(ProcedureName.GenerateUniqueCode);
        return result;
    }
    /// <summary>
    /// دریافت موجودیت شخص به همراه روابط خانوادگی
    /// </summary>
    /// <param name="queryParam"></param>
    /// <returns></returns>
    public async Task<Person> GetPersonWithRelationAsync(string queryParam) {
        var dbSet = _dbContext.Set<Person>().AsNoTracking();

        var result = await dbSet.Where(x =>
                (x.NationalCode == queryParam || x.YektaCode == queryParam || x.Mobile == queryParam ||
                 x.UniqueCode.ToString() == queryParam || x.Id.ToString() == queryParam) && !x.Deleted)
                      .Include(x => x.FatherPerson)
                      .Include(x => x.MotherPerson)
                      .Include(x => x.MarriageHusbandPeople)
                      .ThenInclude(m => m.WifePerson)
                      .Include(x => x.MarriageWifePeople)
                      .ThenInclude(m => m.HusbandPerson)
                     .FirstOrDefaultAsync();
        return result;
    }
}

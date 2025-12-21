namespace Csis.Admission.Application.Common.Interfaces.Repositories.ImamJamaat;

/// <inheritdoc />
public interface IMosqueRepository : IRepository<Mosque>
{
    /// <summary>
    /// دریافت اطلاعات کامل مسجد شامل امام جماعت، آدرس و فعالیت
    /// </summary>
    /// <param name="mosqueId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Mosque> GetMosqueFullInfoAsync(int mosqueId, CancellationToken cancellationToken = default);
}

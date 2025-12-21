using Csis.Admission.Application.Features.People.Dtos;
using Csis.Admission.Domain.Entities;

namespace Csis.Admission.Application.Common.Interfaces.Repositories;

/// <summary>
/// مخزن موجودیت شخص
/// </summary>
public interface IPersonRepository : IRepository<Person>
{
    /// <summary>
    /// دریافت کد یکتا از توالی
    /// </summary>
    /// <returns></returns>
    Task<int> GetNextYektaCodeAsync();

    /// <summary>
    /// دریافت شخص با نسبت خانوادگی
    /// </summary>
    /// <param name="nationalCode"></param>
    /// <returns></returns>
    Task<Person> GetPersonWithRelationAsync(string nationalCode);
}

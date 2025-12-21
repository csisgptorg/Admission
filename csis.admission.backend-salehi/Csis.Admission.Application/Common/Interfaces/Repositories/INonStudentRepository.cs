using Csis.Admission.Domain.Entities;

namespace Csis.Admission.Application.Common.Interfaces.Repositories;

/// <summary>
/// مخزن موجودیت غیر طلبه
/// </summary>
public interface INonStudentRepository : IRepository<NonStudent, long>
{
}

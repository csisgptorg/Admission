namespace Csis.Admission.Application.Common.Interfaces.Repositories.Student;

public interface ICaseFillingRequestRepository : IRepository<CaseFillingRequest, long>
{
    Task DeleteAll();
}

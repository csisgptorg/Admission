using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Employments.Commands;

/// <summary>شناسایی موردی اشتغال</summary>
public record IdentifyStudentEmploymentRequestCommand(int Codm, string EmployeeName, string Description) : IRequest<long>;
internal sealed class IdentifyStudentEmploymentRequestCommandHandler(IRepository<StudentEmployment> studentEmployementRepository,
    IStudentRepository studentRepository,
    IRequestService requestService)
    : IRequestHandler<IdentifyStudentEmploymentRequestCommand, long>
{
    public async Task<long> Handle(IdentifyStudentEmploymentRequestCommand command, CancellationToken cancellationToken) {
        var student = await studentRepository.GetByCodm(command.Codm) ?? throw new CommandValidationException("طلبه یافت نشد");

        var employment = await studentEmployementRepository.GetOneAsync(x => x.Codm == command.Codm, cancellationToken: cancellationToken);

        if (employment != null && employment.IsEmployee.HasValue && employment.IsEmployee.Value) {
            throw new CommandValidationException("امکان شناسایی موردی اشتغال برای فرد شاغل وجود ندارد");
        }

        var requestCommand = new CreateRequestCommand(command, RequestFlow.DirectRegistration, RequestType.IdentifyStudentEmployment);
        var result = await requestService.Create(requestCommand, cancellationToken);
        return result;
    }
}

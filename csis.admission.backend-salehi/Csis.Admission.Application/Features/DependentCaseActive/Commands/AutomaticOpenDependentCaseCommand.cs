using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models.Repository;

namespace Csis.Admission.Application.Features.DependentCaseActive.Commands;

/// <summary>
/// باز کردن پرونده تکفل (اجرای واقعی بدون Request)
/// </summary>
public sealed record AutomaticOpenDependentCaseCommand : IRequest<long>
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>شناسه تکفل</summary>
    public long DependentId { get; set; }

    /// <summary>دهک تکفل</summary>
    public short? Decile { get; set; }

    /// <summary>شناسه درخواست</summary>
    public long? RequestId { get; set; }

    /// <summary>خلاصه اطلاعات تجمیعی</summary>
    public object? TajmieiSummary { get; set; }
}

internal sealed class OpenDependentCaseCommandHandler(
    IRepository<DependentSummary, long> dependentSummaryRepository,
    ICurrentUserService currentUserService,
    IStudentRepository studentRepository)
    : IRequestHandler<AutomaticOpenDependentCaseCommand, long>
{
    public async Task<long> Handle(AutomaticOpenDependentCaseCommand request, CancellationToken cancellationToken) {
        // دریافت اطلاعات تکفل
        var dependent = await dependentSummaryRepository.GetOneAsync(
            x => x.Id == request.DependentId && x.Codm == request.Codm,
            cancellationToken: cancellationToken);

        // باز کردن پرونده تکفل - علت: طلاق
        var activateModel = new UpdateStudentDependentCaseActiveStatusPrc {
            Codm = request.Codm,
            DependentId = request.DependentId,
            ActiveReason = DependentActiveReasonEnum.Divorce,
            DataSource = (await currentUserService.PersonnelId()).HasValue ? DataSource.Employee : DataSource.Student,
            ApplicationId = 66, // شناسه برنامه پذیرش
            UserId = (await currentUserService.GetUserIdAsync()) ?? 0,
            PersonnelId = await currentUserService.PersonnelId(),
        };

        await studentRepository.UpdateDependentCaseActiveStatus(activateModel);

        return request.DependentId;
    }
}

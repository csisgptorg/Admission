using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Teaches.Commands;

/// <summary>
/// ایجاد سابقه تدریس برای طلبه
/// </summary>
public sealed record CreateTeachRequestCommand : BaseCommandDto<CreateTeachRequestCommand, Teach>, IRequest<long>
{
    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; init; }

    /// <summary>
    /// Province
    /// </summary>
    public short? ProvinceId { get; init; }

    /// <summary>
    /// City
    /// </summary>
    public short? CityId { get; init; }

    /// <summary>
    /// EducationYear
    /// </summary>
    public short? EducationYearId { get; init; }

    /// <summary>
    /// EducationSemester
    /// </summary>
    public EducationSemester? EducationSemester { get; init; }

    /// <summary>
    /// مقطع تحصیلی که در آن تدریس میشود
    /// </summary>
    public TeachEducationLevel? EducationLevel { get; init; }

    /// <summary>
    /// Lesson
    /// </summary>
    public string Lesson { get; init; }

    /// <summary>
    /// SchoolId
    /// </summary>
    public short? SchoolId { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public short? WeekSession { get; init; }

    /// <summary>
    /// مرکز حوزوی
    /// </summary>
    public ApprovalCenter? ApprovalCenter { get; init; }

    /// <summary>
    /// شناسه تبلیغ در مرکز حوزوی
    /// </summary>
    public string RecordIdInApprovalCenter { get; init; }
}

internal sealed class CreateTeachRequestCommandHandler(IRequestService requestService) : IRequestHandler<CreateTeachRequestCommand, long>
{
    public async Task<long> Handle(CreateTeachRequestCommand request, CancellationToken cancellationToken) {
        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.CreateTeach);
        return await requestService.Create(requestCommand, cancellationToken);
    }
}

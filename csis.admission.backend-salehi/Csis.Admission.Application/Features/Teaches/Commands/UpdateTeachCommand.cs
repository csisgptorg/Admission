using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Teaches.Commands;

/// <summary>
/// بروزرسانی تدریس
/// </summary>
public sealed record UpdateTeachCommand : BaseCommandDto<UpdateTeachCommand, Teach>, IRequest
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// کد مرکز
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// Province
    /// </summary>
    public short? ProvinceId { get; set; }

    /// <summary>
    /// City
    /// </summary>
    public int? CityId { get; set; }

    /// <summary>
    /// EducationYear
    /// </summary>
    public int? EducationYearId { get; set; }

    /// <summary>
    /// EducationSemester
    /// </summary>
    public EducationSemester? EducationSemester { get; set; }

    /// <summary>
    /// مقطع تحصیلی که در آن تدریس میشود
    /// </summary>
    public TeachEducationLevel? EducationLevel { get; set; }

    /// <summary>
    /// Lesson
    /// </summary>
    public string Lesson { get; set; }

    /// <summary>
    /// SchoolId
    /// </summary>
    public int? SchoolId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public short? WeekSession { get; set; }

    /// <summary>
    /// مرکز حوزوی
    /// </summary>
    public ApprovalCenter? ApprovalCenter { get; set; }

    /// <summary>
    /// شناسه تبلیغ در مرکز حوزوی
    /// </summary>
    public string RecordIdInApprovalCenter { get; set; }
}

internal sealed class UpdateTeachCommandHandler(IRepository<Teach> teachRepo) : IRequestHandler<UpdateTeachCommand>
{
    public async Task Handle(UpdateTeachCommand request, CancellationToken cancellationToken) {
        var teach = await teachRepo.GetByIdAsTrackingAsync(request.Id, cancellationToken: cancellationToken)
            ?? throw new RecordNotFoundException<Teach>(request.Id);

        request.ToEntity(teach);
        await teachRepo.UpdateAsync(teach, true, cancellationToken);
    }
}

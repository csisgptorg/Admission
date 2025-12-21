using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.UniversityEducations.Commands;

/// <summary>تحصیلات دانشگاهی</summary>
public sealed record class CreateDependentUniversityEducationNonIranianRequestCommand : IRequest
{
    /// <summary>شناسه تکفل</summary>
    public long DependentId { get; init; }

    /// <summary>در حال تحصیل</summary>
    public bool InStudy { get; init; }

    /// <summary>مدرک تحصیلی</summary>
    public StudyLevel? StudyLevel { get; init; }

    /// <summary>رشته</summary>
    public string CourseStudy { get; init; }

    /// <summary>نوع دانشگاه</summary>
    public UniversityTypeEnum? UniversityType { get; init; }

    /// <summary>نام دانشگاه</summary>
    public string UniversityName { get; init; }

    /// <summary>نام استان</summary>
    public string ProvinceTitle { get; init; }

    /// <summary>تاریخ شروع</summary>
    public string StartDate { get; init; }

    /// <summary>تاریخ پایان</summary>
    public string EndDate { get; init; }

    /// <summary>معدل</summary>
    public double? Average { get; init; }

    /// <summary>تاریخ اعتبار</summary>
    public string ValidityDate { get; init; }

    /// <summary>شناسه فایل</summary>
    public Guid FileId { get; init; }
}

internal sealed class CreateDependentUniversityEducationNonIranianRequestCommandHandler(IRequestService requestService,
    IRepository<Request, long> requestRepo, ICurrentUserService currentUser)
    : IRequestHandler<CreateDependentUniversityEducationNonIranianRequestCommand>
{
    public async Task Handle(CreateDependentUniversityEducationNonIranianRequestCommand command, CancellationToken cancellationToken) {
        if ( await requestRepo.ExistsAsync(x => x.Documents.Any(y => y.FileId == command.FileId)) == true ) {
            throw new CommandValidationException("مستندات تکراری است.");
        }

        if ( await currentUser.IsEmployee() && await currentUser.IsSenior() != true ) {
            throw new CommandValidationException("شما مجوز لازم برای ثبت درخواست تحصیلات دانشگاهی برای را ندارید.");
        }

        var type = Enum.Parse<RequestType>(nameof(CreateDependentUniversityEducationNonIranianRequestCommand).Replace("RequestCommand", ""));
        var request = new CreateRequestCommand(Mapper(command), RequestFlow.StudentToEmployee,type);
        request.AddDocument(command.FileId);
        await requestService.Create(request, cancellationToken);
    }

    private CreateDependentUniversityEducationCommand Mapper(CreateDependentUniversityEducationNonIranianRequestCommand command) {
        return new CreateDependentUniversityEducationCommand {
            DependentId = command.DependentId,
            InStudy = command.InStudy,
            StudyLevel = command.StudyLevel,
            CourseStudy = command.CourseStudy,
            UniversityType = command.UniversityType,
            UniversityName = command.UniversityName,
            ProvinceTitle = command.ProvinceTitle,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            Average = command.Average,
            ValidityDate = command.ValidityDate,
        };
    }
}

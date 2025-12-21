using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.UniversityEducations.Commands;

/// <summary>مدل اکشن</summary>
public sealed record class CreateDependentUniversityEducationIranianRequestCommandAction(long DependentId, string TraceCode);

/// <summary>تحصیلات دانشگاهی تکفل</summary>
public sealed record class CreateDependentUniversityEducationIranianRequestCommand : IRequest
{
    /// <summary>کد مرکز خدمات</summary>
    public long DependentId { get; set; }

    /// <summary>کد رهگیری</summary>
    public string TraceCode { get; init; }

    /// <summary>تایید</summary>
    public bool Confirmed { get; init; }
}

internal sealed class CreateDependentUniversityEducationIranianRequestCommandHandler(IRequestService requestService,
    ICurrentUserService currentUser, ICsisWsmService wsmService, IRepository<DependentSummary,long> dependentRepo)
    : IRequestHandler<CreateDependentUniversityEducationIranianRequestCommand>
{
    public async Task Handle(CreateDependentUniversityEducationIranianRequestCommand command, CancellationToken cancellationToken) {

        if ( await currentUser.IsEmployee() && await currentUser.IsSenior() != true ) {
            throw new CommandValidationException("شما مجوز لازم برای ثبت درخواست تحصیلات دانشگاهی برای را ندارید.");
        }

        var dependent =await dependentRepo.GetOneAsync(x=>x.Id==command.DependentId,false,cancellationToken);
        var inquiryRequest = new InquiryCertificateModel(dependent.Codm, dependent.Id,dependent.NationalCode, command.TraceCode);
        var inquiryResult = await wsmService.GetInquiryCertificate(inquiryRequest, cancellationToken);

        if ( command.Confirmed ) {
            var type = Enum.Parse<RequestType>(nameof(CreateDependentUniversityEducationIranianRequestCommand).Replace("RequestCommand", ""));
            var requestCommandPayload = inquiryResult.Select(x => Mapper(dependent.Id, x)).ToArray();
            var requestCommand = new CreateBatchDependentUniversityEducationCommand(dependent.Id, requestCommandPayload);
            var request = new CreateRequestCommand(requestCommand, RequestFlow.DirectRegistration, type);
            await requestService.Create(request, cancellationToken);

        } else {
            throw new ConfirmedValidationException(inquiryResult);
        }
    }

    private CreateDependentUniversityEducationCommand Mapper(long dependentId, ResponseInquiryCertificateDto inquiry) {
        return new CreateDependentUniversityEducationCommand {
            DependentId = dependentId,
            InStudy = inquiry.InStudy ?? false,
            StudyLevel = inquiry.StudyLevel,
            CourseStudy = inquiry.CourseStudy,
            UniversityType = inquiry.UniversityType,
            UniversityName = inquiry.University,
            ProvinceTitle = inquiry.Province,
            StartDate = inquiry.StartDate,
            EndDate = inquiry.EndDate,
            Average = inquiry.Average,
            ValidityDate = null
        };
    }
}

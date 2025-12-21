using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.UniversityEducations.Commands;

/// <summary></summary>
public sealed record class CreateStudentUniversityEducationIranianRequestCommandAction(int? Codm, string TraceCode);

/// <summary>تحصیلات دانشگاهی</summary>
public sealed record class CreateStudentUniversityEducationIranianRequestCommand : IRequest
{
    /// <summary>کد مرکز خدمات</summary>
    public int? Codm { get; set; }

    /// <summary>کد رهگیری</summary>
    public string TraceCode { get; init; }

    /// <summary>تایید</summary>
    public bool Confirmed { get; init; }
}

internal sealed class CreateStudentUniversityEducationIranianRequestCommandHandler(IRequestService requestService,
    ICurrentUserService currentUser, ICsisWsmService wsmService, IRepository<StudentSummary> studentRepo)
    : IRequestHandler<CreateStudentUniversityEducationIranianRequestCommand>
{
    public async Task Handle(CreateStudentUniversityEducationIranianRequestCommand command, CancellationToken cancellationToken) {
        _ = await Common.Utilities.SetCodm(command, currentUser);

        if ( await currentUser.IsEmployee() && await currentUser.IsSenior() != true ) {
            throw new CommandValidationException("شما مجوز لازم برای ثبت درخواست تحصیلات دانشگاهی را ندارید.");
        }

        var nationalCode = (await studentRepo.GetOneAsync(x => x.Codm == command.Codm, cancellationToken: cancellationToken)).NationalCode;
        var inquiryRequest = new InquiryCertificateModel(command.Codm.Value,null, nationalCode, command.TraceCode);
        var inquiryResult = await wsmService.GetInquiryCertificate(inquiryRequest, cancellationToken);

        if ( command.Confirmed ) {
            var type = Enum.Parse<RequestType>(nameof(CreateStudentUniversityEducationIranianRequestCommand).Replace("RequestCommand", ""));
            var requestCommandPayload = inquiryResult.Select(x => Mapper(command.Codm.Value, x)).ToArray();
            var requestCommand = new CreateBatchStudentUniversityEducationCommand(command.Codm.Value, requestCommandPayload);
            var request = new CreateRequestCommand(requestCommand, RequestFlow.DirectRegistration, type);
            await requestService.Create(request, cancellationToken);

        } else {
            throw new ConfirmedValidationException(inquiryResult);
        }
    }

    private CreateStudentUniversityEducationCommand Mapper(int codm, ResponseInquiryCertificateDto inquiry) {
        return new CreateStudentUniversityEducationCommand {
            Codm = codm,
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

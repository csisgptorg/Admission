using Csis.Notification;
using System.Text.Json.Serialization;
using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.Employments.Commands;

/// <summary>بروز رسانی وضعیت اشتغال</summary>
public sealed record class CreateOrUpdateStudentEmploymentCommand : BaseCommandDto<CreateOrUpdateStudentEmploymentCommand, StudentEmployment>, IRequest<int>
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>آیا فرد دارای درآمد است؟</summary>
    public bool? HasIncome { get; init; }

    /// <summary>آیا فرد کارمند است؟</summary>
    public bool? IsEmployee { get; init; }

    /// <summary>نام محل کار فرد</summary>
    public string EmployeeName { get; init; }

    /// <summary>آدرس محل کار فرد</summary>
    public string EmployeeAddress { get; init; }

    /// <summary>آیا فرد دارای درآمد کافی می‌باشد؟</summary>
    public bool? HasSufficientIncome { get; init; }

    /// <summary>آیا فرد دارای بیمه پایه دیگری است؟</summary>
    public bool? HasAnotherBaseInsurance { get; init; }

    /// <summary>نام محل بیمه پایه دیگر</summary>
    public string InsurancePlaceName { get; init; }

    /// <summary>آدرس محل بیمه پایه دیگر</summary>
    public string InsurancePlaceAddress { get; init; }

    /// <summary>آیا فرد دارای بیمه تکمیلی دیگری است؟</summary>
    public bool? HasAnotherSupInsurance { get; init; }

    /// <summary>آیا فرد در حوزه مشغول به کار است؟</summary>
    public bool? IsEmployeeInHowze { get; init; }

    /// <summary>نوع اشتغال در حوزه (شناسه نوع اشتغال)</summary>
    public EmploymentHowzeType? HowzeTypeId { get; init; }

    /// <summary>آیا فرد بازنشسته است؟</summary>
    public bool? IsRetried { get; init; }

    /// <summary>دهک درآمدی</summary>
    public short? Decile { get; init; }

    /// <summary>شناسه نوع بیمه اشتغال</summary>
    public short? InsuranceTypeId { get; init; }

    /// <summary>مرجع یا منبع اطلاعات اشتغال  Json </summary>
    [JsonIgnore]
    public EmploymentReference? Reference { get; init; } = EmploymentReference.KhodeEzhari;

    /// <summary>شناسه درخواست</summary>
    public long? RequestId { get; set; }
}

internal sealed class CreateOrUpdateStudentEmploymentCommandHandler(
    IRepository<StudentEmployment> repo,
    ICsisNotificationService csisNotificationService,
    IRepository<UploadedFile> uploadRepo,
    IRepository<RequestDocument, long> documentRepo)
    : IRequestHandler<CreateOrUpdateStudentEmploymentCommand, int>
{
    public async Task<int> Handle(CreateOrUpdateStudentEmploymentCommand command, CancellationToken cancellationToken) {

        var employment = await repo.GetOneAsTrackingAsync(x => x.Codm == command.Codm, cancellationToken: cancellationToken);

        if ( employment is null ) {
            var newEmployment = command.ToEntity();
            await repo.InsertAsync(newEmployment, cancellationToken: cancellationToken);
            return newEmployment.Id;

        } else {
            await repo.UpdateAsync(command.ToEntity(employment), cancellationToken: cancellationToken);
            return employment.Id;
        }

        //var message = new StringBuilder();
        //message.Append("طلبه گرامی اشتغال شما در");
        //message.Append($" {command.EmployeeName} ");
        //message.Append("ثبت شد.");

        //var sendMessage = new SendMessageToStudent(message.ToString(), [command.Codm], [DeliveryChannelEnum.Sms]);
        //await _csisNotificationService.SendMessageToStudent(sendMessage, cancellationToken);
    }
}

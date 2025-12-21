using Csis.Authorization.Services;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Protests.Commands;

/// <summary>
/// ثبت اعتراض بر اساس کد مرکز
/// </summary>
public sealed record CreateProtestByCodmRequestCommand : IRequest
{
    /// <summary>  کد مرکز </summary>
    public int Codm { get; set; }

    /// <summary>شناسه فیلد مورد اعتراض</summary>
    public ProtestFormTitle FieldId { get; init; }

    /// <summary>
    ///  این فیلد برای اعتراضات مربوط به سوابق مسکن می‌باشد (BeingLandlord, HousingBuySellHistory, PersonalHousingHistory)
    /// </summary>
    public bool? HasHousingHistory { get; init; }

    /// <summary>شرح مورد اعتراض</summary>
    public string FieldDescription { get; init; }

    /// <summary>
    /// لیست فایل‌ها (مدارک دیگر)
    /// </summary>
    public RequestDocumentDto[] Documents { get; init; } = [];
}

internal sealed class CreateProtestByCodmRequestCommandHandler(IStudentRepository protestRepository,
    ICurrentUserService currentUser,
    IRequestService requestService)
    : IRequestHandler<CreateProtestByCodmRequestCommand>
{
    public async Task Handle(CreateProtestByCodmRequestCommand request, CancellationToken cancellationToken) {

        _ = await Common.Utilities.SetCodm(request, currentUser);
        var protest = await protestRepository.GetProtests(request.Codm);

        if ( protest.All(x => x.FieldId != request.FieldId) ) {
            throw new CommandValidationException("امکان ثبت اعتراض برای این فرم وجود ندارد.");
        }

        var requestType = Protest.GetRequestType(request.FieldId);
        var createdRequest = new CreateRequestCommand(request, RequestFlow.StudentToEmployee, requestType) { Documents = request.Documents };
        await requestService.Create(createdRequest, cancellationToken);
    }
}

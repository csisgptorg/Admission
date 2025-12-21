using System.Text;
using Csis.Utilities;
using Csis.Authorization.Services;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Addresses.Commands;

/// <summary>ثبت درخواست بروز رسانی آدرس</summary>
public sealed record CreateOrUpdateStudentAddressEmployeeRequestCommand : IRequest
{
    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>کد پستی</summary>
    public long PostalCode { get; set; }

    /// <summary>شهرک</summary>
    public string Township { get; set; }

    /// <summary>خیابان اصلی</summary>
    public string Avenue { get; set; }

    /// <summary>خیابان فرعی</summary>
    public string Street { get; set; }

    /// <summary>کوچه اصلی</summary>
    public string Alley { get; set; }

    /// <summary>کوچه فرعی</summary>
    public string Lane { get; set; }

    /// <summary>بلوک</summary>
    public string Block { get; set; }
}

//TODO
internal sealed class CreateOrUpdateStudentAddressEmployeeRequestCommandHandler(
    ICsisWsmService wsmService,
    IRequestService requestService)
    : IRequestHandler<CreateOrUpdateStudentAddressEmployeeRequestCommand>
{
    public async Task Handle(CreateOrUpdateStudentAddressEmployeeRequestCommand command, CancellationToken cancellationToken) {

        var wsmAddress = await wsmService.GetAddressByPostalCode(command.Codm, command.PostalCode, cancellationToken);
        var request = wsmAddress.GetAddressEmployee(command.Codm, command.PostalCode);
        request.Township = command.Township;
        request.Avenue = command.Avenue;
        request.Street = command.Street;
        request.Alley = command.Alley;
        request.Lane = command.Lane;
        request.Block = command.Block;
        request.ConfirmDate = PersianDateTime.Now.ToString();

        var requestCommand = new CreateRequestCommand(request, RequestFlow.DirectRegistration, RequestType.CreateOrUpdateStudentAddressEmployee);
        _ = await requestService.Create(requestCommand, cancellationToken);
    }
}

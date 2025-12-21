using System.Text;
using Csis.Utilities;
using Csis.Authorization.Services;
using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Features.Addresses.Commands;

/// <summary>ثبت درخواست بروز رسانی آدرس</summary>
public sealed record CreateOrUpdateStudentAddressRequestCommand : IRequest
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

    /// <summary>کد مرکز خدماتی که آدرس را تایید خواهند کرد</summary>
    public int[] ConfirmedStudentCodms { get; set; } = null;

    /// <summary>تایید</summary>
    public bool? Confirmed { get; set; }
}

//TODO
internal sealed class CreateOrUpdateStudentAddressRequestCommandHandler(
    ICsisWsmService wsmService,
    IRequestService requestService,
    IRepository<Address> addressRepo,
    IStudentDataService studentService,
     ICurrentUserService currentUser)
    : IRequestHandler<CreateOrUpdateStudentAddressRequestCommand>
{
    public async Task Handle(CreateOrUpdateStudentAddressRequestCommand command, CancellationToken cancellationToken) {
        _ = await Common.Utilities.SetCodm(command, currentUser);

        var wsmAddress = await wsmService.GetAddressByPostalCode(command.Codm, command.PostalCode, cancellationToken);
        var request = wsmAddress.GetAddress(command.Codm, command.PostalCode);
        request.Township = command.Township;
        request.Avenue = command.Avenue;
        request.Street = command.Street;
        request.Alley = command.Alley;
        request.Lane = command.Lane;
        request.Block = command.Block;
        request.ConfirmDate = PersianDateTime.Now.ToString();
        request.ConfirmedStudentCodms = command.ConfirmedStudentCodms;
        var flow = request.RequiresDualStudentApproval == true
            ? RequestFlow.DualStudents
            : RequestFlow.DirectRegistration;

        var address = await addressRepo.GetOneAsync(x => x.Codm == command.Codm, false, cancellationToken);
        if ( command.Confirmed != true ) {
            var differences = Common.Utilities.GetDifferences(address, request.ToEntity());
            throw new ConfirmedValidationException(differences);
        }

        await DualStudentsValidator(command, flow);

        var requestCommand = new CreateRequestCommand(request, flow);
        requestCommand.AddDualStudentsCodm(command.ConfirmedStudentCodms);
        _ = await requestService.Create(requestCommand, cancellationToken);
    }

    private async Task DualStudentsValidator(CreateOrUpdateStudentAddressRequestCommand command, RequestFlow flow) {
        if ( flow != RequestFlow.DualStudents ) { return; }

        if ( flow == RequestFlow.DualStudents && command.ConfirmedStudentCodms.Distinct().Count() < 2 ) {
            throw new CommandValidationException(
                "برای ثبت این آدرس، تأیید دو طلبه الزامی است. لطفاً طلاب تأییدکننده را معرفی فرمایید.");
        }

        var codms = command.ConfirmedStudentCodms.Select(x => x.ToString()).ToArray();
        var students = await studentService.GetStudentGroupInfoAsync(codms);
        if ( students.Count != 2 ) {
            var notFoundCodms = codms.Except(students.Select(x => x.Codm.ToString())).ToArray();
            var message = new StringBuilder();

            if ( notFoundCodms.Length == 1 ) {
                message.Append("طلاب با کد");
                message.Append($" {notFoundCodms.First()} ");
                message.Append("یافت نشد.");

            } else {
                message.Append("طلاب با کدهای");
                message.Append($" {string.Join(" و ", notFoundCodms)} ");
                message.Append("یافت نشدند.");
            }

            throw new CommandValidationException(message.ToString());
        }
    }
}

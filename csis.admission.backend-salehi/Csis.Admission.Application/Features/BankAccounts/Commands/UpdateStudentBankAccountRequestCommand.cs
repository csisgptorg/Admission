using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Features.Students.Dtos;
using Csis.Authorization.Services;

namespace Csis.Admission.Application.Features.BankAccounts.Commands;

/// <summary>درخواست بروز رسانی حساب بانکی</summary>
public sealed record UpdateStudentBankAccountRequestCommand : IRequest
{
    /// <summary>کد مرکزخدمات</summary>
    public int Codm { get; set; }

    /// <summary>شماره حساب</summary>
    public string BankAccountNumber { get; init; }

    /// <summary>شناسه فایل</summary>
    public Guid? FileId { get; init; }
}

internal sealed class UpdateRequestStudentBankAccountCommandHandler(ICsisWsmService wsmService, ICurrentUserService currentUser,
    IRequestService requestService, IStudentRepository studentRepository, IRepository<StudentSummary> studentSummaryRepository
    ) : IRequestHandler<UpdateStudentBankAccountRequestCommand>
{
    public async Task Handle(UpdateStudentBankAccountRequestCommand command, CancellationToken cancellationToken) {
        _ = await Common.Utilities.SetCodm(command, currentUser);
        var isEmployee = await currentUser.IsEmployee();
        var student = await studentRepository.GetByCodm(command.Codm);

        await Validate(command, student, cancellationToken);

        var flow = isEmployee switch {
            true => RequestFlow.DirectRegistration,
            false => student.Citizenship == Citizenship.Iranian ? RequestFlow.DirectRegistration : RequestFlow.StudentToEmployee
        };

        var requestCommand = new CreateRequestCommand(command, flow);
        if ( command.FileId.HasValue ) {
            requestCommand.AddDocument(command.FileId.Value);
        }
        await requestService.Create(requestCommand, cancellationToken);
    }

    private async Task Validate(UpdateStudentBankAccountRequestCommand command, StudentDto student, CancellationToken cancellationToken) {
        var isPersonnelSenior = await currentUser.IsSenior();
        if ( (student.Citizenship == Citizenship.NonIranian && command.FileId == null) && !isPersonnelSenior ) {
            throw new CommandValidationException("بارگزاری مدرک برای شهروندان غیر ایرانی الزامی می باشد.");
        }

        if ( await studentSummaryRepository.ExistsAsync(
                x => x.Codm != command.Codm && x.BankAccountNumber == command.BankAccountNumber,
                cancellationToken: cancellationToken) ) {
            throw new CommandValidationException("شماره حساب قبلا ثبت شده است.");
        }

        Common.Utilities.ValidateSibaAccountNumber(command.BankAccountNumber);

        var sibaAccountNumber = new ValidateSibaAccountNumberRequest(student.Codm, student.NationalCode, command.BankAccountNumber);
        if ( student.Citizenship == Citizenship.Iranian && !await wsmService.ValidateSibaAccountNumber(sibaAccountNumber, cancellationToken) ) {
            throw new CommandValidationException("شماره حساب با کد ملی مطابقت ندارد.");
        }
    }
}

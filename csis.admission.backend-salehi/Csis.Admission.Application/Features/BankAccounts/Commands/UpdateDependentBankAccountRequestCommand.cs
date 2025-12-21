using Csis.Authorization.Services;
using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Interfaces.Repositories;

namespace Csis.Admission.Application.Features.BankAccounts.Commands;

/// <summary>درخواست بروز رسانی حساب بانکی</summary>
public sealed record class UpdateDependentBankAccountRequestCommand : IRequest
{
    /// <summary>شناسه تکفل</summary>
    public long DependentId { get; init; }

    /// <summary>کد مرکز خدمات</summary>
    public int Codm { get; set; }

    /// <summary>شماره حساب بانکی</summary>
    public string BankAccountNumber { get; init; }

    /// <summary>شناسه فایل</summary>
    public Guid? FileId { get; init; }
}

internal sealed class UpdateDependentBankAccountRequestCommandHandler(ICsisWsmService wsmService,
    IRequestService requestService,
    IRepository<DependentSummary, long> dependentRepo,
     ICurrentUserService currentUser) : IRequestHandler<UpdateDependentBankAccountRequestCommand>
{

    public async Task Handle(UpdateDependentBankAccountRequestCommand command, CancellationToken cancellationToken) {
        _ = await Common.Utilities.SetCodm(command, currentUser);
        var isEmployee = await currentUser.IsEmployee();

        var dependent = await dependentRepo.GetOneAsync(x => x.Id == command.DependentId, cancellationToken: cancellationToken);
        await Validate(command, dependent, cancellationToken);

        var flow = isEmployee switch {
            true => RequestFlow.DirectRegistration,
            false => dependent.Citizenship == Citizenship.Iranian ? RequestFlow.DirectRegistration : RequestFlow.StudentToEmployee
        };

        var requestCommand = new CreateRequestCommand(command, flow);
        if ( command.FileId.HasValue ) {
            requestCommand.AddDocument(command.FileId.Value);
        }
        await requestService.Create(requestCommand, cancellationToken);
    }

    private async Task Validate(UpdateDependentBankAccountRequestCommand command, DependentSummary dependent, CancellationToken cancellationToken) {
        var isPersonnelSenior = await currentUser.IsSenior();
        if ( (dependent.Citizenship == Citizenship.NonIranian && !command.FileId.HasValue) && !isPersonnelSenior ) {
            throw new CommandValidationException("بارگزاری مدرک برای شهروندان غیر ایرانی الزامی می باشد.");
        }

        if ( await dependentRepo.ExistsAsync(
                x => x.Codm != command.Codm && x.BankAccountNumber == command.BankAccountNumber,
                cancellationToken: cancellationToken) ) {
            throw new CommandValidationException("شماره حساب قبلا ثبت شده است.");
        }

        Common.Utilities.ValidateSibaAccountNumber(command.BankAccountNumber);

        var sibaAccountNumber = new ValidateSibaAccountNumberRequest(dependent.Codm, dependent.NationalCode, command.BankAccountNumber);
        if ( dependent.Citizenship == Citizenship.Iranian && !await wsmService.ValidateSibaAccountNumber(sibaAccountNumber, cancellationToken) ) {
            throw new CommandValidationException("شماره حساب با کد ملی مطابقبت ندارد.");
        }
    }
}

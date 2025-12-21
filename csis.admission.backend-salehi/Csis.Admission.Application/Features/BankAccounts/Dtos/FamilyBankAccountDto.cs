namespace Csis.Admission.Application.Features.BankAccounts.Dtos;
/// <inheritdoc/>
public class FamilyBankAccountDto
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public long? DependentId { get; set; }

    /// <inheritdoc/>
    public string FirstName { get; set; }

    /// <inheritdoc/>
    public string LastName { get; set; }

    /// <inheritdoc/>
    public string NationalCode { get; set; }

    /// <inheritdoc/>
    public string BankAccountNumber { get; set; }

    /// <inheritdoc/>
    public Nationality Citizenship { get; set; }

    /// <inheritdoc/>
    public string KhadamatCardNumber { get; set; }
}


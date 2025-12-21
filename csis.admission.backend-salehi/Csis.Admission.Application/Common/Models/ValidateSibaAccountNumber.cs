namespace Csis.Admission.Application.Common.Models;

/// <inheritdoc/>
public sealed record ValidateSibaAccountNumberRequest(int Codm, string NationalIdentifier, string AccountNumber);

/// <inheritdoc/>
public sealed record ValidateSibaAccountNumberResponse(bool IsMatched);


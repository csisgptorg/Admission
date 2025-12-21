namespace Csis.Admission.Application.Common.Models;

/// <inheritdoc/>
public sealed class ValidateMobileOwnershipRequest
{
    /// <inheritdoc/>
    public ValidateMobileOwnershipRequest(string nationalCode, string phoneNumber, IdentityType identificationType= IdentityType.NationalCode) {
        NationalCode = nationalCode;
        PhoneNumber = phoneNumber;
        IdentificationType = identificationType;
    }

    /// <inheritdoc/>
    public string NationalCode { get; set; }

    /// <inheritdoc/>
    public string PhoneNumber { get; set; }

    /// <inheritdoc/>
    public IdentityType IdentificationType { get; set; }

    /// <inheritdoc/>
    public enum IdentityType
    {
        /// <inheritdoc/>
        NationalCode = 0,

        /// <inheritdoc/>
        Passport = 1,

        /// <summary>آمایش</summary>
        Asylum = 2,

        /// <summary>پناهندگی</summary>
        Refugee = 3,

        /// <summary>هویت</summary>
        Identity = 4,

        /// <summary>شناسه ملی</summary>
        NationalID = 5,

        /// <summary>شماره فراگیر گذرنامه</summary>
        PassportUniversalNumber = 6
    }
}

/// <inheritdoc/>
public record ValidateMobileOwnershipResponse(bool IsValid, string Comment, int Code);

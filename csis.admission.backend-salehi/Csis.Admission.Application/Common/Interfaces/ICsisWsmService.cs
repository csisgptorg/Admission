using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Common.Models.ValidateSpousalRelationship;

namespace Csis.Admission.Application.Common.Interfaces;

/// <inheritdoc/>
public interface ICsisWsmService
{
    /// <inheritdoc/>
    Task<bool> ValidateMobileOwnership(ValidateMobileOwnershipRequest request, CancellationToken cancellation);

    /// <inheritdoc/>
    Task<bool> ValidateSibaAccountNumber(ValidateSibaAccountNumberRequest request, CancellationToken cancellation);

    /// <inheritdoc/>
    Task<ValidateSpousalRelationshipResponse.Result> ValidateSpousalRelationship(ValidateSpousalRelationshipRequest request, CancellationToken cancellation);

    /// <inheritdoc/>
    Task<ValidateParentChildRelationshipResponse> ValidateParentChildRelationship(ValidateParentChildRelationshipRequest request, CancellationToken cancellation);

    /// <inheritdoc/>
    Task<GetIdentityInfoByNationalCodeResponse> GetIdentityInfoByNationalCode(GetIdentityInfoByNationalCodeRequest request, CancellationToken cancellation);

    /// <inheritdoc/>
    Task<GetIdentityInfoByNationalCodeResponse> GetIdentityInfoByNationalCode(GetIdentityInfoByNationalCodeRequestApiM request, CancellationToken cancellation);

    /// <inheritdoc/>
    Task<GetAddressByPostalCodeResponse> GetAddressByPostalCode(int codm, long postalCode, CancellationToken cancellation);

    /// <inheritdoc/>
    Task<ResponseInquiryCertificateDto[]> GetInquiryCertificate(InquiryCertificateModel request, CancellationToken cancellation);

    /// <summary>دریافت اطلاعات طلبه در مراکز حوزوی</summary>
    Task<string> GetStudentInfoInMarakezHowzavi(StudentInfoInMarakezHowzaviRequest request, CancellationToken cancellation);

    /// <summary> تایید کد یکتا غیر ایرانی </summary>
    /// اتباع
    Task<ValidateNonIranianYektaCodeResponse> ValidateNonIranianYektaCode(int codm, string yektaCode,
        CancellationToken cancellation);


    /// <summary> تایید رابطه غیر ایرانی </summary>
    /// المصطفی
    Task<ValidateNonIranianRelationshipResponse> ValidateNonIranianRelationship(ValidateNonIranianRelationshipRequest request, CancellationToken cancellation);

    /// <summary>دریافت اطلاعات هویتی از المصطفی</summary>
    Task<GetIdentityInfoByYektaCodeResponse> GetIdentityInfoByYektaCode(string yektaCod, CancellationToken cancellation);

    /// <summary>اعتبارسنجی مالکیت شبا</summary>
    Task<ValidateShebaOwnershipResponse> ValidateShebaOwnerShip(string nationalCode, string accountNumber, CancellationToken cancellation);
    /// <summary>دریافت خلاصه تجمیعی</summary>
    Task<object> GetTajmieiSummary(string nationalCode, CancellationToken cancellation);
    /// <summary> دریافت تصویر ایرانی ها از ثبت احوال</summary>
    Task<GetIranianImageFromSabteAhvalResponse> GetIranianImageFromSabteAhval(string nationalCode,string birthDate, CancellationToken cancellation);   
}

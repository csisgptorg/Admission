using Csis.Admission.Application.Common.Models;

namespace Csis.Admission.Application.Common.Interfaces;

/// <summary>اطلاعات شناسنامه ای</summary>
public partial interface IBirthCertService
{
    /// <summary>ایرانی</summary>
    Task<BirthCertInfo> Iranian(string nationalCode, string birthDate, CancellationToken cancellation);

    /// <summary>غیر ایرانی</summary>
    Task<NonIranianBirthCertInfo> NonIranian(string yektaCode, CancellationToken cancellation);
}

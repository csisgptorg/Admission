/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Interfaces;

/// <summary>
/// سرویس کار با آدرس آی پی
/// </summary>
public interface IIpAddressService
{
    /// <summary>
    /// دریافت آدرس آی پی ارسال کننده درخواست
    /// </summary>
    /// <returns></returns>
    string GetIpAddress();
}

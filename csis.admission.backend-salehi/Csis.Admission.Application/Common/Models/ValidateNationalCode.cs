using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csis.Admission.Application.Common.Models;
/// <summary>
/// مدل درخواست برای اعتبارسنجی کد ملی
/// </summary>
/// <param name="NationalCode"></param>
/// <param name="BirthDate"></param>
public record ValidateNationalCodeRequest(string NationalCode,string BirthDate);

/// <summary>
/// مدل پاسخ برای اعتبارسنجی کد ملی
/// </summary>
public class ValidateNationalCodeResponse
{
    /// <summary>  
    /// کد ملی شخص  
    /// </summary>  
    public string Nin { get; init; }

    /// <summary>  
    /// اعتبارسنجی کد ملی  
    /// </summary>  
    /// <returns>نتیجه اعتبارسنجی</returns>  
    public bool NinValidation() => Nin.HasValue();
}

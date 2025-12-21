namespace Csis.Admission.Application.Common.Helpers;

/// <summary>
/// Helper class for Iranian national code validation.
/// </summary>
public static class NationalCodeHelper
{
    /// <summary>
    /// بررسی صحت کد ملی ایرانی
    /// </summary>
    /// <param name="nationalCode">کد ملی</param>
    /// <returns>true اگر کد ملی معتبر باشد، در غیر این صورت false</returns>
    public static bool IsValidNationalCode(string nationalCode)
    {
        if (string.IsNullOrWhiteSpace(nationalCode)) {
            return false;
        }

        // حذف فاصله‌ها و کاراکترهای غیرضروری
        nationalCode = nationalCode.Trim().Replace(" ", "").Replace("-", "");

        // بررسی طول کد ملی (باید 10 رقم باشد)
        if (nationalCode.Length != 10) {
            return false;
        }

        // بررسی اینکه همه کاراکترها عدد باشند
        if (!nationalCode.All(char.IsDigit)) {
            return false;
        }

        // بررسی کدهای ملی نامعتبر (مثل 0000000000, 1111111111, ...)
        return !IsInvalidRepeatedDigits(nationalCode) &&
               // محاسبه رقم کنترل
               IsValidCheckDigit(nationalCode);
    }

    /// <summary>
    /// بررسی کدهای ملی با ارقام تکراری نامعتبر
    /// </summary>
    /// <param name="nationalCode">کد ملی</param>
    /// <returns>true اگر کد ملی دارای ارقام تکراری نامعتبر باشد</returns>
    private static bool IsInvalidRepeatedDigits(string nationalCode)
    {
        var invalidCodes = new[]
        {
            "0000000000", "1111111111", "2222222222", "3333333333", "4444444444",
            "5555555555", "6666666666", "7777777777", "8888888888", "9999999999"
        };

        return invalidCodes.Contains(nationalCode);
    }

    /// <summary>
    /// بررسی صحت رقم کنترل کد ملی با استفاده از الگوریتم استاندارد
    /// </summary>
    /// <param name="nationalCode">کد ملی</param>
    /// <returns>true اگر رقم کنترل صحیح باشد</returns>
    private static bool IsValidCheckDigit(string nationalCode)
    {
        var digits = nationalCode.Select(c => int.Parse(c.ToString())).ToArray();
        
        // محاسبه مجموع وزن‌دار 9 رقم اول
        var sum = 0;
        for (int i = 0; i < 9; i++)
        {
            sum += digits[i] * (10 - i);
        }

        // محاسبه باقیمانده تقسیم بر 11
        var remainder = sum % 11;
        var checkDigit = digits[9];

        // بررسی رقم کنترل
        if (remainder < 2)
        {
            return checkDigit == remainder;
        }

        return checkDigit == 11 - remainder;
    }

    /// <summary>
    /// الگوی regex برای اعتبارسنجی اولیه کد ملی
    /// </summary>
    public const string NationalCodeRegexPattern = @"^\d{10}$";
}

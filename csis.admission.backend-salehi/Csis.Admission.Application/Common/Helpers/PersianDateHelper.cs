using System.Globalization;

namespace Csis.Admission.Application.Common.Helpers;

/// <summary>
///  تبدیل تاریخ شمسی به میلادی
/// </summary>
public static class PersianDateHelper
{
    private static readonly PersianCalendar Pc = new PersianCalendar();

    public static string ToGregorianDashed(string persianYmd) {
        if ( !TryParsePersianYmd(persianYmd, out var dt) )
            throw new ArgumentException("Persian date is invalid.", nameof(persianYmd));

        return dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
    }

    public static bool TryParsePersianYmd(string input, out DateTime gregorian) {
        gregorian = default;

        if ( string.IsNullOrWhiteSpace(input) )
            return false;

        // فقط رقم‌ها رو نگه می‌داریم (ارقام فارسی/انگلیسی هر دو اوکی)
        var digits = new string(input.Where(char.IsDigit).ToArray());
        if ( digits.Length != 8 )
            return false;

        int y = int.Parse(digits[..4], CultureInfo.InvariantCulture);
        int m = int.Parse(digits.Substring(4, 2), CultureInfo.InvariantCulture);
        int d = int.Parse(digits.Substring(6, 2), CultureInfo.InvariantCulture);

        try {
            // ساخت تاریخ میلادی از تقویم شمسی
            gregorian = new DateTime(y, m, d, Pc);
            return true;
        } catch {
            return false; // تاریخ نامعتبر (مثلاً 13990232)
        }
    }

    public static DateOnly ToGregorianDateOnly(string persianYmd) {
        if ( !TryParsePersianYmd(persianYmd, out var dt) )
            throw new ArgumentException("Persian date is invalid.", nameof(persianYmd));

        return DateOnly.FromDateTime(dt);
    }
}

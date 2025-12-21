using Csis.Utilities;
using System.Globalization;

namespace Csis.Admission.Application.Extensions;

///<inheritdoc/>
public static class IntExtensions
{
    ///<inheritdoc/>
    public static string IntDateToString(this int input) {
        return input <= 0 ?  null : input.ToString("####/##/##");
    }

    ///<inheritdoc/>
    public static string IntDateToString(this int? input) {
        return !input.HasValue ?  null : IntDateToString(input.Value);
    }


    /// <summary>تبدیل تاریخ شمسی عددی به تاریخ میلادی رشته ای</summary>
    public static string IntDateToGregorianStingDate(this int? input) {
        return !input.HasValue ? null : IntDateToGregorianStingDate(input.Value);
    }

    /// <summary>تبدیل تاریخ شمسی عددی به تاریخ میلادی رشته ای</summary>
    public static string IntDateToGregorianStingDate(this int input) {
        var year = int.Parse(input.ToString().Substring(0, 4));
        var month = int.Parse(input.ToString().Substring(4, 2));
        var day = int.Parse(input.ToString().Substring(6, 2));
        var dateTime = new PersianCalendar().ToDateTime(year, month, day, 0, 0, 0, 0);
        return dateTime.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
    }
}

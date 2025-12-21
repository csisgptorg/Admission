namespace Csis.Admission.Application.Common;

public static partial class Constants
{
    public static partial class Db
    {
        /// <summary>
        /// Default schema used for database tables
        /// </summary>
        public const string DefaultSchema = "dbo";
    }

    public static partial class Folders
    {
    }

    /// <summary>رجیکس تاریخ معتبر</summary>
    public const string StringDateFormatRegex = "^(12|13|14)\\d{2}[-/](0[1-9]|1[0-2])[-/](0[1-9]|[12][0-9]|3[01])$";

    /// <summary>کد یکتا معتبر</summary>
    public const string YektaCodeFormatRegex = @"^9\d{9}$";
}

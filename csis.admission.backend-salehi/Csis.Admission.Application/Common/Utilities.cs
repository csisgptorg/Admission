using Csis.Admission.Application.Common.Dtos.RequestService;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Interfaces.Repositories;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Extensions;
using Csis.Admission.Application.Features.Employments.Queries;
using Csis.Admission.Application.Features.Students.Commands;
using Csis.Admission.Domain.Entities;
using Csis.Admission.Domain.Enums;
using Csis.Authorization;
using Csis.Authorization.Services;
using Csis.Utilities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Csis.Admission.Application.Common;

/// <inheritdoc/>
public static partial class Utilities//TODO فایل خیلی شلوغ شده و باید شکسته شود همراه با دسته بندی
{
    /// <inheritdoc/>
    public static int? StringDateToInt(object input) {

        var value = input.ToString();
        if ( !string.IsNullOrWhiteSpace(value) ) {
            value = value.Replace("/", "").Replace("-", "");
        }

        if ( int.TryParse(value, out var result) ) {
            return result;
        }

        return default;
    }

    /// <summary>پر کردن کد مرکز خدمات در مدل</summary>
    public async static Task<int?> SetCodm(object obj, ICurrentUserService currentUser) {
        var codmProp = obj.GetType().GetProperty("Codm");
        var codm = await currentUser.Codm();

        if ( codm > 0 ) {
            codmProp.SetValue(obj, codm);
            return codm;
        }
        return null;
    }

    /// <summary>پر کردن کد مرکز خدمات در مدل</summary>
    public async static Task SetLogParam(RepoCommandLogParam model, ICsisAuthenticatedUserService authenticatedUserService, IHttpContextAccessor context) {
        int? userId = 0;
        int? personnelId = 0;
        int? applicationId = 0;
        try {
            userId = await authenticatedUserService.GetUserIdAsync();
            if ( userId.HasValue ) {
                model.UserId = userId.Value;
            }

            personnelId = await authenticatedUserService.GetPersonnelIdAsync();
            if ( personnelId > 0 ) {
                model.PersonnelId = personnelId;
            }

            applicationId = context.HttpContext.GetSourceApplicationId();
            if ( applicationId > 0 ) {
                model.ApplicationId = applicationId;
            }
        } catch ( Exception e ) {
            Console.WriteLine(e);
        }

        var codmProp = model.GetType().GetProperty("Codm");
        _ = int.TryParse(codmProp.GetValue(model).ToString(), out var codm);
        if ( personnelId > 0 ) {
            model.DataSource = DataSource.Employee;
        } else if ( applicationId > 0 ) {
            model.DataSource = DataSource.WebService;
        } else if ( codm > 0 ) {
            model.DataSource = DataSource.Student;
        }
    }

    private static readonly JsonSerializerOptions _serializerOptions = new() { PropertyNameCaseInsensitive = true };
    /// <inheritdoc/>
    public static TType JsonToModel<TType>(string json) => JsonSerializer.Deserialize<TType>(json, _serializerOptions);

    /// <inheritdoc/>
    public static string MaskMobileNumber(string mobile) {
        return string.Concat(mobile.AsSpan(7), "****", mobile.AsSpan(0, 3));
    }

    /// <summary>محاسبه سن به روز، ماه و سال</summary>
    public static string CalculateAgeDetailed(int? birthDate, int? deathDate) {
        if ( !birthDate.HasValue ) {
            return null;
        }

        var today = deathDate.HasValue ? PersianDateTime.Parse(deathDate.IntDateToString()) : PersianDateTime.Now;
        var birthDatePersian = PersianDateTime.Parse(birthDate.IntDateToString());

        var years = today.Year - birthDatePersian.Year;
        var months = today.Month - birthDatePersian.Month;
        var days = today.Day - birthDatePersian.Day;

        if ( days < 0 ) {
            months--;
            // Get days in previous Persian month
            var previousMonth = today.Month == 1 ? 12 : today.Month - 1;
            var yearForPreviousMonth = today.Month == 1 ? today.Year - 1 : today.Year;

            if ( previousMonth <= 6 ) {
                days += 31;
            } else if ( previousMonth <= 11 ) {
                days += 30;
            } else {
                // Month 12: check for leap year
                var calendar = new System.Globalization.PersianCalendar();
                days += calendar.IsLeapYear(yearForPreviousMonth) ? 30 : 29;
            }
        }

        if ( months < 0 ) {
            years--;
            months += 12;
        }

        var result = new List<string>();
        if ( years > 0 )
            result.Add($"{years} سال");
        if ( months > 0 )
            result.Add($"{months} ماه");
        if ( days > 0 )
            result.Add($"{days} روز");

        return string.Join("، ", result);
    }

    /// <summary>محاسبه سن به سال</summary>
    public static int? CalculateAge(int? birthDate, int? deathDate) {
        if ( !birthDate.HasValue ) {return null;}

        var today = deathDate.HasValue ? PersianDateTime.ParseToDateTime(deathDate.IntDateToString()) : DateTime.Today;
        var birthDateString = PersianDateTime.ParseToDateTime(birthDate.IntDateToString());

        var years = today.Year - birthDateString.Year;
        return years;
    }

    /// <summary>محاسبه سن به سال</summary>
    public static int? CalculateAge(string birthDate, string deathDate) {
        if ( string.IsNullOrEmpty(birthDate) ) { return null; }

        var today = !string.IsNullOrEmpty(deathDate) ? PersianDateTime.ParseToDateTime(deathDate) : DateTime.Today;
        var birthDateString = PersianDateTime.ParseToDateTime(birthDate);

        var years = today.Year - birthDateString.Year;
        return years;
    }

    /// <summary>تبدیل روز به روز، ماه و سال</summary>
    public static string ConvertDaysToDurationString(int? totalDays) {

        if ( totalDays is null )
            return "";

        var years = totalDays / 365;
        var remainingDays = totalDays % 365;
        var months = remainingDays / 30;
        var days = remainingDays % 30;

        var result = new List<string>();
        if ( years > 0 )
            result.Add($"{years} سال");
        if ( months > 0 )
            result.Add($"{months} ماه");
        if ( days > 0 )
            result.Add($"{days} روز");

        return string.Join("، ", result);
    }

    /// <summary>فرمت نام شیء SQL</summary>
    public static string FormatSqlObjectName(string name) {
        var parts = name.Split('.');
        return string.Join(".", parts.Select(p => $"[{p}]"));
    }

    /// <summary>اعتبار سنجی حساب سیبا</summary>
    public static void ValidateSibaAccountNumber(string accountNumber) {

        var validationException = new CommandValidationException("شماره حساب سیبا نامعتبر است.");

        if ( accountNumber.Length < 13 ) {
            throw validationException;
        }

        var mainPart = accountNumber[..12];
        var checkDigit = accountNumber.Substring(12, 1);
        var prefixTwoDigits = accountNumber[..2];
        var prefixThreeDigits = accountNumber[..3];

        if ( accountNumber.Substring(10, 2) != "00" ) {
            throw validationException;
        }

        if ( !(prefixTwoDigits == "01" || prefixTwoDigits == "02" || prefixTwoDigits == "03" || prefixTwoDigits == "99" ||
              prefixTwoDigits == "41" || prefixTwoDigits == "25" || prefixTwoDigits == "31" || prefixThreeDigits == "217") ) {
            throw validationException;
        }

        var weightedSum = (5 * int.Parse(mainPart.Substring(11, 1))) +
                          (7 * int.Parse(mainPart.Substring(10, 1))) +
                          (13 * int.Parse(mainPart.Substring(9, 1))) +
                          (17 * int.Parse(mainPart.Substring(8, 1))) +
                          (19 * int.Parse(mainPart.Substring(7, 1))) +
                          (23 * int.Parse(mainPart.Substring(6, 1))) +
                          (29 * int.Parse(mainPart.Substring(5, 1))) +
                          (31 * int.Parse(mainPart.Substring(4, 1))) +
                          (37 * int.Parse(mainPart.Substring(3, 1))) +
                          (41 * int.Parse(mainPart.Substring(2, 1))) +
                          (43 * int.Parse(mainPart.Substring(1, 1))) +
                          (47 * int.Parse(mainPart[..1]));

        weightedSum %= 11;
        if ( weightedSum == 1 ) {
            throw validationException;
        }

        var calculatedCheckDigit = 11 - weightedSum;
        if ( calculatedCheckDigit == 11 ) {
            calculatedCheckDigit = 0;
        }

        if ( Math.Abs(calculatedCheckDigit - double.Parse(checkDigit)) >= 0.002 ) {
            throw validationException;
        }
    }

    public static string NormalizeFa(string? s) {
        if ( string.IsNullOrWhiteSpace(s) )
            return string.Empty;
        var t = s.Trim()
            .Replace('\u064A', '\u06CC') // ي -> ی
            .Replace('\u0643', '\u06A9') // ك -> ک
            .Replace("\u200C", "")       // ZWNJ
            .Replace("\u200E", "")       // LRM
            .Replace("\u200F", "");      // RLM

        // حذف حرکات
        var marks = new[] { '\u064B', '\u064C', '\u064D', '\u064E', '\u064F', '\u0650', '\u0651', '\u0652' };
        foreach ( var m in marks )
            t = t.Replace(m.ToString(), string.Empty);

        // تک‌فاصله کردن
        while ( t.Contains("  ") )
            t = t.Replace("  ", " ");
        return t;
    }

    /// <summary>لیست تفاوت های میان دو شیء</summary>
    public sealed record PropertyDifference(string Name, object PreviousValue, object CurrentValue);
    /// <summary>لیست تفاوت های میان دو شیء</summary>
    public static List<PropertyDifference> GetDifferences<T>(T oldObj, T newObj) {
        var differences = new List<PropertyDifference>();

        var type = typeof(T);
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach ( var prop in properties ) {
            if ( prop.Name == "UpdatedOn" || prop.Name == "CreatedOn" || prop.Name == "Id" || prop.Name == "RequestId" ||
                prop.Name == "Request" || prop.Name == "CreatedById" || prop.Name == "LastUpdatedById" || prop.Name == "Codm" ||
                prop.Name == "House" ||
                (type.Name == "StudentEmployment" && (prop.Name == "Decile" || prop.Name == "Reference"))
                ) { continue; }

            // Handle Tenant property separately to show its differences
            if ( prop.Name == "Tenant" ) {
                var oldTenant = oldObj != null ? prop.GetValue(oldObj) : null;
                var newTenant = prop.GetValue(newObj);

                if ( oldTenant != null || newTenant != null ) {
                    var tenantDifferences = CompareTenantObjects(oldTenant, newTenant);
                    differences.AddRange(tenantDifferences);
                }

                continue;
            }

            var previousValue = oldObj != null ? prop.GetValue(oldObj) : null;
            var CurrentValue = prop.GetValue(newObj);

            if ( (previousValue == null && CurrentValue == null) || (previousValue != null && previousValue.Equals(CurrentValue)) ) {
                continue;
            }

            differences.Add(new PropertyDifference(prop.Name, previousValue, CurrentValue));
        }

        return differences;
    }

    /// <summary>مقایسه دو شیء Tenant</summary>
    private static List<PropertyDifference> CompareTenantObjects(object oldTenant, object newTenant) {
        var differences = new List<PropertyDifference>();

        if ( oldTenant == null && newTenant == null ) {
            return differences;
        }

        var tenantType = oldTenant?.GetType() ?? newTenant?.GetType();
        var properties = tenantType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach ( var prop in properties ) {
            if ( prop.Name == "UpdatedOn" || prop.Name == "CreatedOn" || prop.Name == "Id" || prop.Name == "HouseId" ||
                 prop.Name == "House" || prop.Name == "CreatedById" || prop.Name == "LastUpdatedById" || prop.Name == "TempId" ||
                 prop.Name == "AuditDataSource" || prop.Name == "AuditRequestId" || prop.Name == "Codm" ||
                 prop.Name == "AuditPersonId" || prop.Name == "Deleted" ) {
                continue;
            }

            var oldValue = oldTenant != null ? prop.GetValue(oldTenant) : null;
            var newValue = newTenant != null ? prop.GetValue(newTenant) : null;

            if ( (oldValue == null && newValue == null) || (oldValue != null && oldValue.Equals(newValue)) ) {
                continue;
            }

            if ( prop.Name is "StartDate" or "EndDate" ) {
                if ( oldValue is int oldInt ) {
                    if ( newValue is int newInt ) {
                        differences.Add(new PropertyDifference(prop.Name, oldInt.IntDateToString(), newInt.IntDateToString()));
                        continue;
                    }
                    differences.Add(new PropertyDifference(prop.Name, oldInt.IntDateToString(), newValue));
                } else if ( oldValue is null && newValue is int newInt ) {
                    differences.Add(new PropertyDifference(prop.Name, oldValue, newInt.IntDateToString()));
                    continue;
                } else {
                    differences.Add(new PropertyDifference(prop.Name, oldValue, newValue));
                }
                continue;
            }

            differences.Add(new PropertyDifference(prop.Name, oldValue, newValue));
        }

        return differences;
    }

    /// <summary>مد دولوپ</summary>
    public static bool IsDevMode(HttpContext httpContext) {

        var host = httpContext?.Request?.Host.Value?.ToLower();
        if ( !string.IsNullOrEmpty(host) && host.Contains("dev.csis") ) {
            return true;
        }

        if ( httpContext?.Connection?.RemoteIpAddress?.ToString() == "127.0.0.1" ||
                System.Net.IPAddress.IsLoopback(httpContext?.Connection?.RemoteIpAddress) )
            return true;

        return Debugger.IsAttached;
    }

    #region In Extensions
    public static bool In(this string value, params string[] items) {
        if ( items == null )
            throw new ArgumentNullException("in items");

        return items.Contains(value);
    }




    public static bool In(this int value, params int[] items) {
        if ( items == null )
            throw new ArgumentNullException("in items");

        return items.Contains(value);
    }

    public static bool In(this int? value, params int?[] items) {
        if ( items == null )
            throw new ArgumentNullException("in items");

        return items.Contains(value);
    }



    public static bool In(this short value, params short[] items) {
        if ( items == null )
            throw new ArgumentNullException("in items");

        return items.Contains(value);
    }

    public static bool In(this short? value, params short?[] items) {
        if ( items == null )
            throw new ArgumentNullException("in items");

        return items.Contains(value);
    }



    public static bool In(this byte value, params byte[] items) {
        if ( items == null )
            throw new ArgumentNullException("in items");

        return items.Contains(value);
    }



    public static bool In(this byte? value, params byte?[] items) {
        if ( items == null )
            throw new ArgumentNullException("in items");

        return items.Contains(value);
    }


    public static bool In<T>(this T value, params T[] items) {
        ArgumentNullException.ThrowIfNull(items);
        return items.Contains(value);
    }

    #endregion

    /// <summary>مقایسه دو شیء و استخراج تفاوت‌های آن‌ها</summary>
    /// <typeparam name="T">نوع شیء</typeparam>
    /// <param name="oldObj">شیء قبلی</param>
    /// <param name="newObj">شیء جدید</param>
    /// <param name="propertiesToSkip">لیست نام پراپرتی‌هایی که باید نادیده گرفته شوند</param>
    /// <returns>لیست تفاوت‌ها</returns>
    public static List<PropertyDifference> CompareObjects<T>(T oldObj, T newObj, HashSet<string> propertiesToSkip = null) {
        var differences = new List<PropertyDifference>();

        if ( oldObj == null && newObj == null ) {
            return differences;
        }

        // Default properties to skip
        var defaultSkipProperties = new HashSet<string> {
            "UpdatedOn", "CreatedOn", "Id", "RequestId", "Request",
            "CreatedById", "LastUpdatedById", "Codm",
            "TempId", "AuditDataSource", "AuditRequestId", "AuditPersonId", "Deleted"
        };

        var skipProperties = propertiesToSkip ?? defaultSkipProperties;
        if ( propertiesToSkip != null ) {
            foreach ( var prop in defaultSkipProperties ) {
                skipProperties.Add(prop);
            }
        }

        var type = typeof(T);
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach ( var prop in properties ) {
            // Skip indexer properties
            if ( prop.GetIndexParameters().Length > 0 ) {
                continue;
            }

            // Always include IsActive property, skip others in the skip list
            if ( prop.Name != "IsActive" && skipProperties.Contains(prop.Name) ) {
                continue;
            }

            var oldValue = oldObj != null ? prop.GetValue(oldObj) : null;
            var newValue = newObj != null ? prop.GetValue(newObj) : null;

            // For IsActive, always add it even if both are null or equal
            if ( prop.Name == "IsActive" ) {
                differences.Add(new PropertyDifference(prop.Name, oldValue, newValue));
                continue;
            }

            // Skip if both values are null
            if ( oldValue == null && newValue == null ) {
                continue;
            }

            // Skip if values are equal
            if ( oldValue != null && oldValue.Equals(newValue) ) {
                continue;
            }

            // Special handling for date properties (int dates)
            if ( prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?) ) {
                if ( prop.Name.EndsWith("Date") || prop.Name.Contains("Date") ) {
                    var oldDateStr = oldValue != null ? ((int) oldValue).IntDateToString() : null;
                    var newDateStr = newValue != null ? ((int) newValue).IntDateToString() : null;
                    differences.Add(new PropertyDifference(prop.Name, oldDateStr, newDateStr));
                    continue;
                }
            }

            differences.Add(new PropertyDifference(prop.Name, oldValue, newValue));
        }

        return differences;
    }

    internal static async Task ValidateSettings(ISettingRepository settingRepository, WebServiceSettingTitle webServiceTitle, RegistrationType registrationType, CancellationToken cancellationToken) {
        var setting = (RegistrationType?) (await settingRepository.GetByKeyAsync(webServiceTitle.ToString())).Value.ToInt();
        if ( setting != registrationType ) {
            var registerTypeText = registrationType == RegistrationType.Manual ? "دستی" : "خودکار";
            var webServiceTitleText = webServiceTitle == WebServiceSettingTitle.Iranian ? "ایرانی" : "غیرایرانی";
            throw new CommandValidationException($"در حال حاضر امکان ثبت افراد به صورت {registerTypeText} از طریق وب سرویس {webServiceTitleText} وجود ندارد.");
        }
    }
}

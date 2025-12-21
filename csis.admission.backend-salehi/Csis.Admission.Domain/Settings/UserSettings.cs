namespace Csis.Admission.Domain.Settings;

/// <summary>
/// تنظیمات کاربر
/// </summary>
public sealed class UserSettings : ISettings<UserSettings>
{
    /// <summary>
    /// استفاده از تم دارک
    /// </summary>
    public bool DarkMode { get; set; }

    /// <summary>
    /// تعداد آیتم های نمایشی در هر صفحه
    /// </summary>
    public int ItemsPerPage { get; set; }

    /// <inheritdoc/>
    public UserSettings GetDefault() {
        return new() {
            DarkMode = false,
            ItemsPerPage = 5
        };
    }
}

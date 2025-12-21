namespace Csis.Admission.Application.Features.Students.Commands;

/// <summary>پارامترهای مربوط به لاگ در پروسیجرهای ثبت کننده دیتا</summary>
public class RepoCommandLogParam
{
    /// <summary>شناسهٔ درخواست</summary>
    public long RequestId { get; set; }

    /// <summary>شناسه کاربر</summary>
    public int UserId { get; set; }

    /// <summary>شناسه پرسنل (در صورت وجود)</summary>
    public int? PersonnelId { get; set; }

    /// <summary>شناسه اپلیکیشن (در صورت وجود)</summary>
    public int? ApplicationId { get; set; }

    /// <summary>منبع ثبت‌کننده</summary>
    public DataSource DataSource { get; set; }
}

namespace Csis.Admission.Domain.Enums;

/// <summary>
/// انواع پژوهش
/// </summary>
public enum ResearchType:short
{
    /// <summary>
    /// تالیف کتاب
    /// </summary>
    BookWriting = 1,

    /// <summary>
    /// ترجمه مقاله
    /// </summary>
    ArticleTranslation,

    /// <summary>
    /// تحقیق
    /// </summary>
    Research,

    /// <summary>
    /// تالیف مقاله
    /// </summary>
    ArticleWriting,

    /// <summary>
    /// ترجمه مقاله (مقدار تکراری، ممکن است اصلاح شود)
    /// </summary>
    ArticleTranslationDuplicate,

    /// <summary>
    /// پروژه پژوهشی
    /// </summary>
    ResearchProject
}

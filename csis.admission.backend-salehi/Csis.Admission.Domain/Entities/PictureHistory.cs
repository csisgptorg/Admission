using Csis.Admission.Domain.Common;

namespace Csis.Admission.Domain.Entities;

/// <summary>
/// سابقه تصاویر پرسنلی
/// </summary>
public sealed class PictureHistory : SoftDeletedBaseEntity
{
    /// <summary>
    /// Codm
    /// </summary>
    public int Codm { get; set; }

    /// <summary>
    /// Picture
    /// </summary>
    public byte[] Picture { get; set; }

    /// <summary>
    /// DateCreated
    /// </summary>
    public int? DateCreated { get; set; }

    /// <summary>
    /// UserId
    /// </summary>
    public int? UserId { get; set; }
}

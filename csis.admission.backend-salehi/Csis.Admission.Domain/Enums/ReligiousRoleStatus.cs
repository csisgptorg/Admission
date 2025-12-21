namespace Csis.Admission.Domain.Enums;

/// <summary>
/// وضعیت نقش آفرینی
/// </summary>
public enum ReligiousRoleStatus : short
{
    /// <summary>نقش آفرینی دارد</summary>
    HasRole = 1,
    /// <summary>نقش آفرینی دارد ولی در قم یا مشهد مانده است</summary>
    HasRoleButInQomOrMashhad = 2,
    /// <summary>نقش آفرینی ندارد</summary>
    HasNoRole = 3
}

namespace Csis.Admission.Domain.Enums;

/// <summary>جزئیات وضعیت سکونت</summary>
public enum HouseStatusItem : short
{
    /// <summary>شهرک های حوزوی</summary>
    SeminaryTownship = 1,

    /// <summary>سازمانی</summary>
    Organizational = 2,

    /// <summary>پدری</summary>
    Paternal = 3,

    /// <summary>منزل همسر</summary>
    SpouseHouse = 4,

    /// <summary>سایر</summary>
    Other = 5
}

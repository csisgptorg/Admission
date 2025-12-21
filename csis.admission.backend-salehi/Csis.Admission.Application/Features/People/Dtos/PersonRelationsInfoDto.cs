using Csis.Admission.Application.Common.Dtos;

namespace Csis.Admission.Application.Features.People.Dtos;

/// <summary>
/// مدل نمایشی اطلاعات روابط شخص
/// </summary>
public sealed record PersonRelationsInfoDto : BaseDto<PersonRelationsInfoDto, Person>
{
    /// <summary>
    /// نام
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// نام خانوادگی
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// کد ملی
    /// </summary>
    public string NationalCode { get; init; }

    /// <summary>
    /// شناسه یکتا
    /// </summary>
    public string YektaCode { get; init; }

    /// <summary>
    /// کد یکتای منحصر به فرد 
    /// </summary>
    public int UniqueCode { get; init; }

    /// <summary>
    /// جنسیت
    /// </summary>
    public Gender Gender { get; init; }

    /// <summary>
    /// رابطه خانوادگی
    /// </summary>
    public FamilyRelationType FamilyRelationType { get; init; }

   
}

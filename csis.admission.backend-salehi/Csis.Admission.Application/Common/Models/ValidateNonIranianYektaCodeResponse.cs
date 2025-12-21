namespace Csis.Admission.Application.Common.Models;

/// <summary>
/// مدل پاسخ برای اعتبارسنجی کد یکتا اشخاص غیر ایرانی
/// </summary>
public class ValidateNonIranianYektaCodeResponse
{
    public int? FidaCode { get; set; }
    public long? UniqeCode { get; set; }
    public DateTime BirthDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FatherName { get; set; }
    public string GrandFatherName { get; set; }
    public int Gender { get; set; }
    public string ShamsiBirthDate { get; set; }
    public string PassportNumber { get; set; }
    public int ProvinceId { get; set; }
    public string ProvinceName { get; set; }
    public int NationalityId { get; set; }
    public string NationalityName { get; set; }
    public int OldFidaCode { get; set; }
    public string FaragirCode { get; set; }
    public int Status { get; set; }
    public long FamilyId { get; set; }
    public bool Exit { get; set; }
    public long ShenasaeiCode { get; set; }
    public int Relative { get; set; }
    public int Education { get; set; }
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public string DeleteAt { get; set; }
    public bool IsRegistered { get; set; }

    /// <summary> </summary>
    public bool IsValid() => UniqeCode.HasValue;
}


using Csis.Admission.Application.Extensions;

namespace Csis.Admission.Application.Common.Models;

/// <inheritdoc/>
public record GetIdentityInfoByNationalCodeRequest(int? Codm, string NationalCode, int BirthDate);
/// <inheritdoc/>
public record GetIdentityInfoByNationalCodeRequestApiM(string NationalCode, string BirthDate, string CardSerial = null);

/// <inheritdoc/>
public class GetIdentityInfoByNationalCodeResponse
{
    /// <summary>دریافت اطلاعات شناسنامه ای</summary>
    public BirthCertInfo BirthCertInfo() {
        var gender = (Gender) int.Parse(Gender);
        var isSadat = Name.Replace("ي", "ی").StartsWith("سید") || FatherName.Replace("ي", "ی").StartsWith("سید");
        var birthCertNumber = ShenasnameNo == "0" ? Nin : ShenasnameNo;
        var age = Utilities.CalculateAge(BirthDate, DeathDate);
        return new BirthCertInfo(Nin, Name, Family, FatherName, isSadat, BirthDate,age.Value, birthCertNumber, ShenasnameSeri,
            int.Parse(ShenasnameSerial), gender, DeathStatus == "1",DeathDate);
    }

    public string Nin { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string FatherName { get; set; }
    public string ShenasnameSeri { get; set; }
    public string ShenasnameNo { get; set; }
    public string ShenasnameSerial { get; set; }

    private string _birthDate;
    public string BirthDate {
        get => int.TryParse(_birthDate, out var val) ? val.IntDateToString() : _birthDate;
        set { _birthDate = value; }
    }
    public string Gender { get; set; }
    public string DeathStatus { get; set; }
    public string OfficeCode { get; set; }
    public string OfficeName { get; set; }
    public string ExceptionMessage { get; set; }

    private string _deathDate;
    public string DeathDate {
        get => int.TryParse(_deathDate, out var val) ? val.IntDateToString() : _deathDate;
        set { _deathDate = value; }
    }

    public string Zipcode { get; set; }
    public string ZipcodeDesc { get; set; }
    public string AddressDesc { get; set; }
    public string SpecialFeild { get; set; }
    public string Birthplace { get; set; }
    public string ShenasnameIssueDate { get; set; }
    public string ShenasnameIssuePlace { get; set; }
    public string CardExpireDate { get; set; }
    public string CardSeri { get; set; }
    public string CardSrno { get; set; }

    public bool IsRegistered { get; set; }
    public int? Id { get; set; }
    public List<GetIdentityInfoByNationalCodeResponseImage> Images { get; set; }
}

/// <inheritdoc/>
public record GetIdentityInfoByNationalCodeResponseImage(string Image, string Type);

/// <summary>اطلاعات شناسنامه ای</summary>
public record BirthCertInfo(string NationalCode,string FirstName, string LastName, string FatherName, bool IsSadat,
    string BirthDate, int Age, string BirthCertNumber, string BirthCertSeri, int BirthCertSerial, Gender Gender, bool IsDead, string DeathDate);

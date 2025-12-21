using Csis.Admission.Application.Common.Interfaces.Repositories.QueryBuilders;

namespace Csis.Admission.Application.Common.Models.QueryBuilders; 
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[QueryBuilder(Label = "شغل و درآمد", Name = "TbEmployee", Tab = Enums.ReportBuilderTab.Student)]
public class StudentEmployment : IQueryBuilderTable
{
    /// <inheritdoc/>
    [QueryBuilder(Label = "دارای درآمدي غير از شهريه و يارانه", Name = "Question1")]
    public bool? HasIncome { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "شاغل", Name = "Question2")]
    public bool? IsEmployee { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "نام محل کار", Name = "NameEmployee")]
    public string EmployeeName { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "آدرس محل کار", Name = "AddressEmployee")]
    public string EmployeeAddress { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "دارای درآمد کافی", Name = "Question3")]
    public bool? HasSufficientIncome { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "نام محل بیمه", Name = "NameInsurancePlace")]
    public string InsurancePlaceName { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "آدرس محل بیمه", Name = "AddressInsurancePlace")]
    public string InsurancePlaceAddress { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "دهک", Name = "Dahak")]
    public short? Decile { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "دارای بيمه غير از مرکز", Name = "Question4")]
    public bool? HasAnotherBaseInsurance { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "دارای بيمه تکمیلی غير از مرکز", Name = "Question5")]
    public bool? HasAnotherSupInsurance { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "بازنشسته", Name = "BazNeshaste")]
    public bool? IsRetried { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "کادر حوزه هاي علميه", Name = "Kadr")]
    public bool? IsEmployeeInHowze { get; set; }

    /// <inheritdoc/>
    [QueryBuilder(Label = "نوع بيمه", Name = "KindBimeh")]
    public EmploymentInsuranceType? InsuranceTypeId { get; set; }
  }

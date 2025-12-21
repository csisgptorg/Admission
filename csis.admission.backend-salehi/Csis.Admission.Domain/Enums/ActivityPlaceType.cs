namespace Csis.Admission.Domain.Enums;

/// <summary>
/// نوع محل فعالیت مسجد (مسجد، حسینیه یا تکیه)
/// </summary>
public enum ActivityPlaceType : short
{
    /// <summary>
    /// مسجد
    /// </summary>
    Mosque = 1,

    /// <summary>
    /// حسینیه یا تکیه
    /// </summary>
    HoseiniyehAndTakieh = 2,

    /// <summary>
    /// بقاع متبرکه
    /// </summary>
     BaghaeMotabarakeh = 3
}

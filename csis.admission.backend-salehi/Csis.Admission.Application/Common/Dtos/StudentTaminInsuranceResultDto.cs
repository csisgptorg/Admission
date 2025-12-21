namespace Csis.Admission.Application.Common.Dtos;

/// <inheritdoc/>
public class StudentTaminInsuranceResultDto
{
    /// <inheritdoc/>
    public int Codm { get; set; }

    /// <inheritdoc/>
    public StatusEnum Status { get; set; }

    /// <inheritdoc/>
    public int TaminNumber { get; set; }

    /// <inheritdoc/>
    public string Description { get; set; }

    /// <inheritdoc/>
    public enum StatusEnum:short {
        /// <summary>فعال</summary>
        Active = 1,
        /// <summary>غیر فعال</summary>
        Inactive = 2,
        /// <summary>بسته</summary>
        Closed = 3,
        /// <summary>ثبت نام موقت</summary>
        TemporaryRegistration = 4
    }
}

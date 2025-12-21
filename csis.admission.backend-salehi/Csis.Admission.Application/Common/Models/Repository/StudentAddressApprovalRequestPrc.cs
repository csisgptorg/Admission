using Csis.Admission.Application.Features.Addresses.Commands;

namespace Csis.Admission.Application.Common.Models;

/// <inheritdoc/>
public class StudentAddressApprovalRequestPrc
{
    /// <inheritdoc/>
    public StudentAddressApprovalRequestPrc(CreateOrUpdateStudentAddressCommand address) {
        Codm = address.Codm;
        ProvinceId = address.ProvinceId;
        CityId = address.CityId;
        PortionId = address.PortionId;
        RuralId = address.RuralId;
        TownId = address.TownId;
    }

    /// <inheritdoc/>
    public int? Codm { get; set; }

    /// <inheritdoc/>
    public short? ProvinceId { get; set; }

    /// <summary>شهرستان</summary>
    public short? CityId { get; set; }

    /// <summary>بخش</summary>
    public short? PortionId { get; set; }

    /// <summary>دهستان</summary>
    public short? RuralId { get; set; }

    /// <summary>شهر</summary>
    public short? TownId { get; set; }
};

using Csis.Admission.Application.Features.Addresses.Commands;

namespace Csis.Admission.Application.Common.Models;

/// <inheritdoc/>
public class GetAddressByPostalCodeResponse
{
    /// <inheritdoc/>
    public AddressModel Address { get; set; }

    /// <inheritdoc/>
    public TB16Model TB16 { get; set; }

    /// <inheritdoc/>
    public bool IsAddressFound { get; set; }

    /// <inheritdoc/>
    public CreateOrUpdateStudentAddressCommand GetAddress(int codm, long postalCode) {
        return new CreateOrUpdateStudentAddressCommand {
            Codm = codm,
            ProvinceId = GetValidValue(TB16.StateId),
            CityId = GetValidValue(TB16.TownShipId),
            PortionId = GetValidValue(TB16.ZoneId),
            TownId = GetValidValue(TB16.LocationId),
            RuralId = GetValidValue(TB16.VillageId),
            Village = Address.LocationType == "روستا" || Address.LocationType == "آبادی" ? Address.Location : null,
            District = Address.Parish,
            Avenue = Address.PreAvenue,
            Alley = Address.Avenue,
            Number = Address.HouseNo.ToString(),
            Complex = Address.BuildingName,
            Unit = Address.SideFloor,
            Floor = Address.FloorNo == "همکف" ? (short) 0 : short.Parse(Address.FloorNo),
            ZipCode = postalCode,
            ProjectCode = 1,
            Flag = true,
            RequiresDualStudentApproval=Address.RequiresDualStudentApproval,
            Township=Address.TownShip,
        };
    }
    /// <inheritdoc/>
    public CreateOrUpdateStudentAddressEmployeeCommand GetAddressEmployee(int codm, long postalCode) {
        return new CreateOrUpdateStudentAddressEmployeeCommand {
            Codm = codm,
            ProvinceId = GetValidValue(TB16.StateId),
            CityId = GetValidValue(TB16.TownShipId),
            PortionId = GetValidValue(TB16.ZoneId),
            TownId = GetValidValue(TB16.LocationId),
            RuralId = GetValidValue(TB16.VillageId),
            Village = Address.LocationType == "روستا" || Address.LocationType == "آبادی" ? Address.Location : null,
            District = Address.Parish,
            Avenue = Address.PreAvenue,
            Alley = Address.Avenue,
            Number = Address.HouseNo.ToString(),
            Complex = Address.BuildingName,
            Unit = Address.SideFloor,
            Floor = Address.FloorNo == "همکف" ? (short) 0 : short.Parse(Address.FloorNo),
            ZipCode = postalCode,
            ProjectCode = 1,
            Flag = true,
            RequiresDualStudentApproval=Address.RequiresDualStudentApproval,
            Township=Address.TownShip,
        };
    }

    /// <inheritdoc/>


    /// <inheritdoc/>
    public record TB16Model(int StateId, int TownShipId, int ZoneId, int LocationId, int VillageId);

    private static short? GetValidValue(int value) => value > 0 ? (short) value : null;
}
    public class AddressModel
    {
        /// <inheritdoc/>
        public string Avenue { get; set; }
        /// <inheritdoc/>
        public string FloorNo { get; set; }
        /// <inheritdoc/>
        public int HouseNo { get; set; }
        /// <inheritdoc/>
        public string Location { get; set; }
        /// <inheritdoc/>
        public string LocationType { get; set; }
        /// <inheritdoc/>
        public string Parish { get; set; }
        /// <inheritdoc/>
        public string PostCode { get; set; }
        /// <inheritdoc/>
        public string PreAvenue { get; set; }
        /// <inheritdoc/>
        public string SideFloor { get; set; }
        /// <inheritdoc/>
        public string State { get; set; }
        /// <inheritdoc/>
        public string TownShip { get; set; }
        /// <inheritdoc/>
        public string Village { get; set; }
        /// <inheritdoc/>
        public string Zone { get; set; }
        /// <inheritdoc/>
        public string BuildingName { get; set; }
        /// <inheritdoc/>
        public string Description { get; set; }
        /// <inheritdoc/>
        public long LocationCode { get; set; }
        /// <inheritdoc/>
        public bool RequiresDualStudentApproval { get; set; }
    }


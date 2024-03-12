using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KatBulgariaCheck.API.Helpers.Settings
{
    public sealed class IndividualObligationsSearchSettings
    {
        public const string ConfigurationSection = "IndividualObligationsSearchSettings";

        public string? PersonalIdentityNumber { get; init; }
        public string? DrivingLicenseNumber { get; init; }
        public string? PersonalIdCardNumber { get; init; }
        public string? VehicleRegistrationNumber { get; init; }
    }
}
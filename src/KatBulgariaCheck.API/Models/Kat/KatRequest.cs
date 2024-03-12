using System.Text.Json.Serialization;
using KatBulgariaCheck.Models.Kat.Enums;

namespace KatBulgariaCheck.Models.Kat
{
    public class KatRequest
    {
        [JsonPropertyName("obligatedPersonType")]
        public ObligatedEntityType ObligatedEntityType { get; set; }

        [JsonPropertyName("additinalDataForObligatedPersonType")]
        public ObligatedIndividualSearchType AdditionalDataProvided { get; set; }

        [JsonPropertyName("mode")]
        public int SearchMode { get; set; }

        [JsonPropertyName("obligedPersonIdent")]
        public string ObligedPersonIdentityNumber { get; set; }

        [JsonPropertyName("drivingLicenceNumber")]
        public string DrivingLicenceNumber { get; set; }

        [JsonPropertyName("personalDocumentNumber")]
        public string PersonalIdCardNumber { get; set; }

        [JsonPropertyName("foreignVehicleNumber")]
        public string VehicleRegistrationNumber { get; set; }
    }
}
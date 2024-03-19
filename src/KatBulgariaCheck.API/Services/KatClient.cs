using FluentResults;
using KatBulgariaCheck.API.Helpers.Settings;
using KatBulgariaCheck.API.Interfaces;
using KatBulgariaCheck.Models.Kat;
using KatBulgariaCheck.Models.Kat.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KatBulgariaCheck.API.Services
{
    public class KatClient : IKatClient
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly IndividualObligationsSearchSettings _individualObligationsSearchSettings;

        public KatClient(HttpClient httpClient,
            IOptions<IndividualObligationsSearchSettings> individualObligationsSearchSettings,
            ILoggerFactory loggerFactory)
        {
            _httpClient = httpClient;
            _individualObligationsSearchSettings = individualObligationsSearchSettings.Value;
            _logger = loggerFactory.CreateLogger<KatClient>();
        }

        public async Task<Result<KatResponse>> GetCompanyObligations()
        {
            return Result.Fail<KatResponse>("Not implemented");
        }

        public async Task<Result<KatResponse>> GetPersonalObligations(ObligatedIndividualSearchType obligatedIndividualSearchType)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://e-uslugi.mvr.bg/api/Obligations/AND");

            var test = new KatRequest()
            {
                SearchMode = 1,
                ObligatedEntityType = ObligatedEntityType.Individual,
                AdditionalDataProvided = ObligatedIndividualSearchType.VehicleNumber,
                VehicleRegistrationNumber = "CA2813XP",
                ObligedPersonIdentityNumber = "9202176667"
            };

            var requestUri = client.BaseAddress + $"?obligatedPersonType={(int)test.ObligatedEntityType}&additinalDataForObligatedPersonType=" +
                $"{(int)test.AdditionalDataProvided}&mode={test.SearchMode}&obligedPersonIdent={test.ObligedPersonIdentityNumber}&foreignVehicleNumber={test.VehicleRegistrationNumber}";

            var response = await client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                _logger.LogInformation($"Response content: {content}");
                return Result.Ok<KatResponse>(new KatResponse());
            }
            else
            {
                _logger.LogError($"Failed to send GET request. Status code: {response.StatusCode}");
                return Result.Ok<KatResponse>(new KatResponse());
            }
        }
    }
}
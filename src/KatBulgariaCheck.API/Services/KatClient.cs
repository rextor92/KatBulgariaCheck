using FluentResults;
using KatBulgariaCheck.API.Interfaces;
using KatBulgariaCheck.Models.Kat;
using KatBulgariaCheck.Models.Kat.Enums;
using Microsoft.Extensions.Logging;

namespace KatBulgariaCheck.API.Services
{
    public class KatClient : IKatClient
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;

        public KatClient(HttpClient httpClient,
            ILoggerFactory loggerFactory)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://e-uslugi.mvr.bg/api/Obligations/AND");
            _logger = loggerFactory.CreateLogger<KatClient>();
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<Result<KatResponse>> GetCompanyObligationsAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return Result.Fail<KatResponse>("Not implemented");
        }

        public async Task<Result<KatResponse>> PersonalCheckByEgnAndDriversLicenseAsync(string egn, string driversLicense)
        {
            _logger.LogInformation($"{nameof(PersonalCheckByEgnAndDriversLicenseAsync)} is invoked.");

            var requestUri = _httpClient.BaseAddress +
                $"?obligatedPersonType={(int)ObligatedEntityType.Individual}" +
                $"&additinalDataForObligatedPersonType={(int)ObligatedIndividualSearchType.VehicleNumber}" +
                $"&mode=1" +
                $"&obligedPersonIdent={egn}&drivingLicenceNumber={driversLicense}";

            var response = await _httpClient.GetAsync(requestUri);
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

        public async Task<Result<KatResponse>> PersonalCheckByEgnAndIdCardAsync(string egn, string idCard)
        {
            _logger.LogInformation($"{nameof(PersonalCheckByEgnAndIdCardAsync)} is invoked.");

            var requestUri = _httpClient.BaseAddress +
                $"?obligatedPersonType={(int)ObligatedEntityType.Individual}" +
                $"&additinalDataForObligatedPersonType={(int)ObligatedIndividualSearchType.VehicleNumber}" +
                $"&mode=1" +
                $"&obligedPersonIdent={egn}&personalDocumentNumber={idCard}";

            var response = await _httpClient.GetAsync(requestUri);
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

        public async Task<Result<KatResponse>> PersonalCheckByEgnAndVehicleRegistrationAsync(string egn, string vehicleRegistration)
        {
            _logger.LogInformation($"{nameof(PersonalCheckByEgnAndVehicleRegistrationAsync)} is invoked.");

            var requestUri = _httpClient.BaseAddress +
                $"?obligatedPersonType={(int)ObligatedEntityType.Individual}" +
                $"&additinalDataForObligatedPersonType={(int)ObligatedIndividualSearchType.VehicleNumber}" +
                $"&mode=1" +
                $"&obligedPersonIdent={egn}&foreignVehicleNumber={vehicleRegistration}";

            var response = await _httpClient.GetAsync(requestUri);
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
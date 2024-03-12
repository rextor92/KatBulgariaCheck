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
            try
            {
                return Result.Fail<KatResponse>("Not implemented");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting company obligations");
                return Result.Fail<KatResponse>("Error occurred while getting company obligations");
            }
        }
    }
}
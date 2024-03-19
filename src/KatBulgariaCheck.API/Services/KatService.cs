using FluentResults;
using KatBulgariaCheck.API.Helpers.Settings;
using KatBulgariaCheck.API.Interfaces;
using KatBulgariaCheck.Models.Kat;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KatBulgariaCheck.API.Services
{
    public class KatService : IKatService
    {
        private readonly ILogger _logger;
        private readonly IKatClient _katClient;
        private readonly IndividualObligationsSearchSettings _individualObligationsSearchSettings;

        public KatService(IKatClient katClient,
            IOptions<IndividualObligationsSearchSettings> individualObligationsSearchSettings,
            ILoggerFactory loggerFactory)
        {
            _katClient = katClient;
            _individualObligationsSearchSettings = individualObligationsSearchSettings.Value;
            _logger = loggerFactory.CreateLogger<KatService>();
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

        public async Task<Result<KatResponse>> GetCompanyObligationsAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return Result.Fail<KatResponse>("Not implemented");
        }

        public async Task<Result<KatResponse>> GetPersonalObligationsAsync()
        {
            _logger.LogInformation($"{nameof(GetPersonalObligationsAsync)}: Getting personal obligations");

            ArgumentNullException.ThrowIfNull(_individualObligationsSearchSettings.PersonalIdentityNumber);

            if (!string.IsNullOrEmpty(_individualObligationsSearchSettings.PersonalIdCardNumber))
            {
                return await _katClient.PersonalCheckByEgnAndIdCardAsync(
                    _individualObligationsSearchSettings.PersonalIdentityNumber,
                    _individualObligationsSearchSettings.PersonalIdCardNumber);
            }
            else if (!string.IsNullOrEmpty(_individualObligationsSearchSettings.DrivingLicenseNumber))
            {
                return await _katClient.PersonalCheckByEgnAndDriversLicenseAsync(
                    _individualObligationsSearchSettings.PersonalIdentityNumber,
                    _individualObligationsSearchSettings.DrivingLicenseNumber);
            }
            else if (!string.IsNullOrEmpty(_individualObligationsSearchSettings.VehicleRegistrationNumber))
            {
                return await _katClient.PersonalCheckByEgnAndVehicleRegistrationAsync(
                    _individualObligationsSearchSettings.PersonalIdentityNumber,
                    _individualObligationsSearchSettings.VehicleRegistrationNumber);
            }
            else
            {
                return Result.Fail<KatResponse>("No search criteria provided");
            }
        }
    }
}
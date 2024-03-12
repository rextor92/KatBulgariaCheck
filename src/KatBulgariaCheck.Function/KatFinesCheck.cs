using KatBulgariaCheck.API.Helpers.Settings;
using KatBulgariaCheck.API.Interfaces;
using KatBulgariaCheck.Models.Kat;
using KatBulgariaCheck.Models.Kat.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace KatBulgariaCheck.Function
{
    public class KatFinesCheck
    {
        private readonly ILogger _logger;
        private readonly IKatClient _katClient;

        public KatFinesCheck(IKatClient katClient,
            ILoggerFactory loggerFactory)
        {
            _katClient = katClient;
            _logger = loggerFactory.CreateLogger<KatFinesCheck>();
        }

        [Function(nameof(TestAsync))]
        public async Task<HttpResponseData> TestAsync([HttpTrigger] HttpRequestData request)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var response = request.CreateResponse(System.Net.HttpStatusCode.OK);
            return response;
        }

        [Function(nameof(KatFinesCheckAsync))]
        public async Task KatFinesCheckAsync([TimerTrigger("0 18 * * MON")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://e-uslugi.mvr.bg/api/Obligations/AND");

            var test = new KatRequest()
            {
                SearchMode = 1,
                ObligatedEntityType = ObligatedEntityType.Individual,
                AdditionalDataProvided = ObligatedIndividualSearchType.VehicleNumber,
                VehicleRegistrationNumber = "CB1001BC",
                ObligedPersonIdentityNumber = "1002003004"
            };

            var requestUri = client.BaseAddress + $"?obligatedPersonType={(int)test.ObligatedEntityType}&additinalDataForObligatedPersonType=" +
                $"{(int)test.AdditionalDataProvided}&mode={test.SearchMode}&obligedPersonIdent={test.ObligedPersonIdentityNumber}&foreignVehicleNumber={test.VehicleRegistrationNumber}";

            var response = await client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                _logger.LogInformation($"Response content: {content}");
            }
            else
            {
                _logger.LogError($"Failed to send GET request. Status code: {response.StatusCode}");
            }
        }
    }
}
using KatBulgariaCheck.API.Interfaces;
using KatBulgariaCheck.Models.Kat.Enums;
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

            await _katClient.GetPersonalObligations(ObligatedIndividualSearchType.VehicleNumber);
        }
    }
}
using FluentValidation;
using KatBulgariaCheck.API.Helpers.Settings;
using KatBulgariaCheck.Function.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddHttpClient();
        services.AddOptionsWithFluentValidation<IndividualObligationsSearchSettings>(IndividualObligationsSearchSettings.ConfigurationSection);

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddValidatorsFromAssembly(typeof(IndividualObligationsSearchSettings).Assembly);
    })
    .Build();

try
{
    host.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    throw;
}
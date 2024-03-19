using FluentValidation;
using KatBulgariaCheck.API.Helpers.Settings;
using KatBulgariaCheck.API.Interfaces;
using KatBulgariaCheck.API.Services;
using KatBulgariaCheck.Function.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddHttpClient();
        services.AddValidatorsFromAssembly(typeof(IndividualObligationsSearchSettings).Assembly);

        services.AddOptionsWithFluentValidation<IndividualObligationsSearchSettings>(IndividualObligationsSearchSettings.ConfigurationSection);
        services.AddScoped<IKatClient, KatClient>();
    })
    .Build();
try
{
    host.Run();
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}
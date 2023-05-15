using AuxLabs.Twitch.Rest;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ZLogger;

var app = ConsoleApp.CreateBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.SetMinimumLevel(LogLevel.Debug);
        logging.AddZLoggerConsole();
    })
    .ConfigureServices(services =>
    {
        services.AddSingleton<TwitchRestClient>();

        //services.AddHostedService<DefaultPollingService>();
    })
    .Build();

app.AddAllCommandType();

await app.RunAsync();
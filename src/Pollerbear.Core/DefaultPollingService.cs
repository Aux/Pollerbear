using AuxLabs.Twitch.Rest;
using Microsoft.Extensions.Logging;
using Pollerbear.Abstractions;

namespace Pollerbear;

public sealed class DefaultPollingService : PollingServiceBase, IDisposable
{
    private readonly TwitchRestClient _twitch;
    private readonly ILogger _logger;

    private Timer? _timer;
    private int _iterations = 0;

    public DefaultPollingService(
        TwitchRestClient twitch,
        ILogger<DefaultPollingService> logger,
        IQueueingService queue, 
        INotifyingService notify, 
        PollingOptions options)
        : base(queue, notify, options) 
    { 
        _twitch = twitch;
        _logger = logger;
    }

    public override Task StartAsync(CancellationToken? cancelToken = null)
    {
        _logger.LogInformation("Starting...");
        cancelToken ??= CancellationToken.None;

        _timer = new Timer(async (state) => await RunAsync(state).ConfigureAwait(false), 
            cancelToken, TimeSpan.Zero, Options.PollingRate);

        _logger.LogInformation("Started");
        return Task.CompletedTask;
    }

    private Task RunAsync(object? state)
    {
        _iterations++;
        _logger.LogInformation("Running poll #{iterations} for this instance", _iterations);

        var cancelToken = (CancellationToken)state!;


        _logger.LogInformation("Polling #{iterations} complete", _iterations);
        return Task.CompletedTask;
    }

    public override Task StopAsync()
    {
        _logger.LogInformation("Stopping...");
        Dispose();

        _logger.LogInformation("Stopped");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();

        if (Queue is IDisposable queue)
            queue.Dispose();
        if (Notify is IDisposable notify)
            notify.Dispose();
    }
}

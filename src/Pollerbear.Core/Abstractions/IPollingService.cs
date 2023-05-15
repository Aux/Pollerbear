namespace Pollerbear.Abstractions;

/// <summary>
///     The main polling handler.
/// </summary>
public interface IPollingService
{
    /// <summary>
    ///     Start an instance of the polling service.
    /// </summary>
    Task StartAsync(CancellationToken? cancelToken = null);

    /// <summary>
    ///     Gracefully shut down the polling service.
    /// </summary>
    Task StopAsync();
}

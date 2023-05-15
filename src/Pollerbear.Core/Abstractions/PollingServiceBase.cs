namespace Pollerbear.Abstractions;

/// <summary>
/// 
/// </summary>
public abstract class PollingServiceBase : IPollingService
{
    /// <summary>
    ///     
    /// </summary>
    protected IQueueingService Queue { get; init; }

    /// <summary>
    ///     
    /// </summary>
    protected INotifyingService Notify { get; init; }

    /// <summary>
    ///     
    /// </summary>
    protected PollingOptions Options { get; init; }

    protected PollingServiceBase(IQueueingService queue, INotifyingService notify, PollingOptions options)
    {
        Queue = queue;
        Notify = notify;
        Options = options;
    }

    public abstract Task StartAsync(CancellationToken? cancelToken = null);
    public abstract Task StopAsync();
}

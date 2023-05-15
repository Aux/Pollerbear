namespace Pollerbear.Abstractions;

/// <summary>
///     Data management for streams being polled.
/// </summary>
public interface IQueueingService
{
    /// <summary>
    ///     Pull the next batch of streams from the queue.
    /// </summary>
    /// <returns>
    ///     At least 1 and at most 100 streams to be requested.
    /// </returns>
    Task<IReadOnlyCollection<string>> TakeAsync();

    /// <summary>
    ///     Attempts to add a specified channel to the queue.
    /// </summary>
    /// <param name="channelId"> The id of the Twitch channel to be configured. </param>
    /// <returns> The same id if successful. </returns>
    Task<string> AddAsync(string channelId);

    /// <summary>
    ///     Attempts to remove a specified channel from the queue.
    /// </summary>
    /// <param name="channelId"> The id of the configured Twitch channel. </param>
    Task RemoveAsync(string channelId);
}

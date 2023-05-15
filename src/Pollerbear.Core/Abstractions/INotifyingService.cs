namespace Pollerbear.Abstractions;

/// <summary>
///     Handles the sending of events through the chosen transport.
/// </summary>
public interface INotifyingService
{
    /// <summary>
    ///     Send a status changed event.
    /// </summary>
    Task SendAsync(string channelId, object payload);
}

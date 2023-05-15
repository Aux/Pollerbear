namespace Pollerbear;

public class PollingOptions
{
    /// <summary>
    ///     The client id of the Twitch application used for polling.
    /// </summary>
    public required string TwitchClientId { get; set; }

    /// <summary>
    ///     The client secret of the Twitch application used for polling.
    /// </summary>
    public required string TwitchClientSecret { get; set; }

    /// <summary>
    ///     The amount of time between each attempt to poll for stream statuses.
    /// </summary>
    /// <remarks>
    ///     The default and minimum value is 5 minutes to reflect the estimated refresh rate of Twitch's rest api cache.
    /// </remarks>
    public TimeSpan PollingRate { get; set; } = TimeSpan.FromMinutes(5);
}
using AuxLabs.Twitch.Rest;
using AuxLabs.Twitch.Rest.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Pollerbear.Services;

public class TwitchAuthorizationService
{
    private readonly TwitchRestClient _twitch;
    private readonly TwitchIdentityApiClient _identity;
    private readonly PollingOptions _options;
    private readonly ILogger _logger;

    private Timer? _timer;
    private TimeSpan _refreshIn;

    public TwitchAuthorizationService(TwitchRestClient twitch, IConfiguration config, ILogger<TwitchAuthorizationService> logger)
    {
        _twitch = twitch;
        _logger = logger;

        config.GetSection("Pollerbear").Bind(_options);
        if (_options is null) 
            throw new NullReferenceException(nameof(_options));

        _identity = new TwitchIdentityApiClient(_options.TwitchClientId, _options.TwitchClientSecret);
    }

    public async Task StartAsync(CancellationToken? cancelToken = null)
    {
        cancelToken ??= CancellationToken.None;

        await RefreshAsync(cancelToken);

        _timer = new Timer(async (state) => await RefreshAsync(cancelToken).ConfigureAwait(false),
            cancelToken, _refreshIn, Timeout.InfiniteTimeSpan);
    }

    private async Task RefreshAsync(CancellationToken? cancelToken)
    {
        var app = await _identity.ValidateAsync();
        await _twitch.ValidateAsync(app.AccessToken);

        if (app.ExpiresInSeconds is not int value)
            throw new ArgumentNullException(nameof(app.ExpiresInSeconds));

        var gracePeriod = value - (value * 0.1);
        _refreshIn = TimeSpan.FromSeconds(gracePeriod);
        _timer?.Change(_refreshIn, Timeout.InfiniteTimeSpan);
    }
}

using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using NightscoutFoundation.Nocturne.Api;
using NightscoutFoundation.Nocturne.Model;
using Nocturne.Widget.Contracts;

namespace Nocturne.Widget.Infrastructure;

/// <summary>
/// Implementation of the Nocturne API client providing HTTP and SignalR connectivity.
/// Uses OAuth Bearer tokens for authentication with automatic token refresh.
/// </summary>
public class NocturneApiClient : INocturneApiClient, IAsyncDisposable
{
    private readonly ICredentialStore _credentialStore;
    private readonly IOAuthService _oauthService;
    private readonly ILogger<NocturneApiClient> _logger;
    private HubConnection? _hubConnection;
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the NocturneApiClient
    /// </summary>
    /// <param name="credentialStore">The credential store for authentication</param>
    /// <param name="oauthService">The OAuth service for token management</param>
    /// <param name="logger">The logger instance</param>
    public NocturneApiClient(
        ICredentialStore credentialStore,
        IOAuthService oauthService,
        ILogger<NocturneApiClient> logger
    )
    {
        _credentialStore = credentialStore ?? throw new ArgumentNullException(nameof(credentialStore));
        _oauthService = oauthService ?? throw new ArgumentNullException(nameof(oauthService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public event EventHandler<DataUpdateEventArgs>? DataUpdated;

    /// <inheritdoc />
    public event EventHandler<TrackerUpdateEventArgs>? TrackerUpdated;

    /// <inheritdoc />
    public event EventHandler<AlarmEventArgs>? AlarmReceived;

    /// <inheritdoc />
    public event EventHandler<AlarmEventArgs>? AlarmCleared;

    /// <inheritdoc />
    public async Task<V4SummaryResponse?> GetSummaryAsync(
        int hours = 0,
        bool includePredictions = false
    )
    {
        try
        {
            if (!await _oauthService.EnsureValidTokenAsync())
            {
                _logger.LogWarning("No valid credentials available for API request");
                return null;
            }

            var credentials = await _credentialStore.GetCredentialsAsync();
            if (credentials is null)
            {
                _logger.LogWarning("No credentials available after token validation");
                return null;
            }

            var api = CreateSummaryApi(credentials);
            return await api.SummaryGetSummaryAsync(hours, includePredictions);
        }
        catch (NightscoutFoundation.Nocturne.Client.ApiException ex) when (ex.ErrorCode == 401)
        {
            _logger.LogDebug("Received 401, attempting token refresh");
            var refreshResult = await _oauthService.RefreshTokenAsync();
            if (!refreshResult.Success)
            {
                _logger.LogWarning("API request failed after token refresh attempt");
                return null;
            }

            var credentials = await _credentialStore.GetCredentialsAsync();
            if (credentials is null) return null;

            try
            {
                var api = CreateSummaryApi(credentials);
                return await api.SummaryGetSummaryAsync(hours, includePredictions);
            }
            catch (Exception retryEx)
            {
                _logger.LogWarning(retryEx, "API request failed after token refresh");
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching summary from Nocturne API");
            return null;
        }
    }

    private static SummaryApi CreateSummaryApi(NocturneCredentials credentials)
    {
        var config = new NightscoutFoundation.Nocturne.Client.Configuration
        {
            BasePath = credentials.ApiUrl.TrimEnd('/')
        };
        config.ApiKey["Authorization"] = credentials.AccessToken;
        config.ApiKeyPrefix["Authorization"] = "Bearer";
        return new SummaryApi(config);
    }

    /// <inheritdoc />
    public async Task ConnectSignalRAsync()
    {
        if (_hubConnection is not null)
        {
            _logger.LogWarning("SignalR connection already exists");
            return;
        }

        // Ensure we have a valid token
        if (!await _oauthService.EnsureValidTokenAsync())
        {
            _logger.LogWarning("No valid credentials available for SignalR connection");
            return;
        }

        var credentials = await _credentialStore.GetCredentialsAsync();
        if (credentials is null)
        {
            _logger.LogWarning("No credentials available for SignalR connection");
            return;
        }

        try
        {
            var hubUrl = $"{credentials.ApiUrl.TrimEnd('/')}/hubs/nocturne";

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(
                    hubUrl,
                    options =>
                    {
                        // Use Bearer token for SignalR authentication
                        options.AccessTokenProvider = async () =>
                        {
                            await _oauthService.EnsureValidTokenAsync();
                            var creds = await _credentialStore.GetCredentialsAsync();
                            return creds?.AccessToken;
                        };
                    }
                )
                .WithAutomaticReconnect()
                .Build();

            ConfigureHubHandlers(_hubConnection);

            await _hubConnection.StartAsync();
            _logger.LogInformation("SignalR connection established");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to SignalR hub");
            _hubConnection = null;
            throw;
        }
    }

    /// <inheritdoc />
    public async Task DisconnectSignalRAsync()
    {
        if (_hubConnection is null)
        {
            return;
        }

        try
        {
            await _hubConnection.StopAsync();
            await _hubConnection.DisposeAsync();
            _logger.LogInformation("SignalR connection closed");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error disconnecting from SignalR hub");
        }
        finally
        {
            _hubConnection = null;
        }
    }

    private void ConfigureHubHandlers(HubConnection connection)
    {
        connection.On<string, long>(
            "DataUpdated",
            (dataType, timestamp) =>
            {
                DataUpdated?.Invoke(
                    this,
                    new DataUpdateEventArgs { DataType = dataType, Timestamp = timestamp }
                );
            }
        );

        connection.On<string, string, double, double>(
            "TrackerUpdated",
            (trackerId, trackerName, ageHours, lifespanHours) =>
            {
                TrackerUpdated?.Invoke(
                    this,
                    new TrackerUpdateEventArgs
                    {
                        TrackerId = trackerId,
                        TrackerName = trackerName,
                        AgeHours = ageHours,
                        LifespanHours = lifespanHours,
                    }
                );
            }
        );

        connection.On<string, string, string, int, bool, long>(
            "AlarmReceived",
            (alarmId, title, message, level, urgent, timestamp) =>
            {
                AlarmReceived?.Invoke(
                    this,
                    new AlarmEventArgs
                    {
                        AlarmId = alarmId,
                        Title = title,
                        Message = message,
                        Level = level,
                        Urgent = urgent,
                        Timestamp = timestamp,
                    }
                );
            }
        );

        connection.On<string, string, string, int, bool, long>(
            "AlarmCleared",
            (alarmId, title, message, level, urgent, timestamp) =>
            {
                AlarmCleared?.Invoke(
                    this,
                    new AlarmEventArgs
                    {
                        AlarmId = alarmId,
                        Title = title,
                        Message = message,
                        Level = level,
                        Urgent = urgent,
                        Timestamp = timestamp,
                    }
                );
            }
        );

        connection.Reconnecting += error =>
        {
            _logger.LogWarning(error, "SignalR connection lost, attempting to reconnect");
            return Task.CompletedTask;
        };

        connection.Reconnected += connectionId =>
        {
            _logger.LogInformation(
                "SignalR reconnected with connection ID {ConnectionId}",
                connectionId
            );
            return Task.CompletedTask;
        };

        connection.Closed += error =>
        {
            if (error is not null)
            {
                _logger.LogError(error, "SignalR connection closed with error");
            }
            else
            {
                _logger.LogInformation("SignalR connection closed");
            }
            return Task.CompletedTask;
        };
    }

    /// <summary>
    /// Disposes the API client and releases resources
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }

        await DisconnectSignalRAsync();
        _disposed = true;

        GC.SuppressFinalize(this);
    }
}

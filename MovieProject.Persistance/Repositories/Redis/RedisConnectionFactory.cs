using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MovieProject.Application.Interfaces;
using MovieProject.Application.Interfaces.Redis;
using MovieProject.Persistance.Repositories.Redis;
using StackExchange.Redis;

namespace MovieProject.Persistance.Repositories.Redis;

public class RedisConnectionFactory : IRedisConnectionFactory, IDisposable
{
    private IConnectionMultiplexer _connection;
    private readonly RedisCacheSettings _settings;
    private DateTime _lastConnectionAttempt = DateTime.MinValue;
    private readonly TimeSpan _reconnectCooldown = TimeSpan.FromMinutes(1);
    private readonly SemaphoreSlim _reconnectSemaphore = new(1, 1);

    public bool IsRedisAvailable { get; private set; }

    public RedisConnectionFactory(IOptions<RedisCacheSettings> options)
    {
        _settings = options.Value;
        TryConnect();
    }

    private void TryConnect()
    {
        try
        {
            var configOptions = ConfigurationOptions.Parse(_settings.ConnectionString);
            configOptions.ConnectTimeout = 2000;
            configOptions.SyncTimeout = 2000;
            configOptions.AbortOnConnectFail = false;

            Console.WriteLine($"Redis'e bağlanma denemesi: {_settings.ConnectionString}");

            _connection = ConnectionMultiplexer.Connect(configOptions);

            if (_connection.IsConnected)
            {
                _connection.ConnectionFailed += OnConnectionFailed;
                _connection.ConnectionRestored += OnConnectionRestored;

                IsRedisAvailable = true;
                Console.WriteLine("✅ Redis bağlantısı BAŞARILI!");
            }
            else
            {
                IsRedisAvailable = false;
                Console.WriteLine("❌ Redis bağlantısı kurulamıyor (IsConnected = false).");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Redis bağlantısı HATA: {ex.Message}");
            _connection = null;
            IsRedisAvailable = false;
        }

        _lastConnectionAttempt = DateTime.UtcNow;
    }

    public IConnectionMultiplexer GetConnection()
    {
        // Bağlantı varsa döndür
        if (_connection?.IsConnected == true)
            return _connection;

        // Otomatik yeniden bağlanma (arka planda)
        _ = Task.Run(async () => await TryReconnectAsync());

        return _connection;
    }

    public async Task<bool> TryReconnectAsync()
    {
        // Çok sık deneme yapma (cooldown kontrolü)
        if (DateTime.UtcNow - _lastConnectionAttempt < _reconnectCooldown)
            return IsRedisAvailable;

        await _reconnectSemaphore.WaitAsync();
        try
        {
            if (!IsRedisAvailable || _connection?.IsConnected != true)
            {
                Console.WriteLine("🔄 Redis yeniden bağlanma denemesi...");

                _connection?.Dispose();

                TryConnect();
            }

            return IsRedisAvailable;
        }
        finally
        {
            _reconnectSemaphore.Release();
        }
    }

    private void OnConnectionFailed(object sender, ConnectionFailedEventArgs e)
    {
        Console.WriteLine($"⚠️ Redis bağlantısı koptu: {e.FailureType} - {e.Exception?.Message}");
        IsRedisAvailable = false;
    }

    private void OnConnectionRestored(object sender, ConnectionFailedEventArgs e)
    {
        Console.WriteLine("✅ Redis bağlantısı yeniden kuruldu!");
        IsRedisAvailable = true;
    }

    public void Dispose()
    {
        _connection?.Dispose();
        _reconnectSemaphore?.Dispose();
    }
}
using MovieProject.Application.Interfaces;
using MovieProject.Application.Interfaces.Redis;
using StackExchange.Redis;
using System.Text.Json;

namespace MovieProject.Persistance.Repositories.Redis;

public class RedisCacheService : IRedisCacheService
{
    private readonly IRedisConnectionFactory _factory;

    public RedisCacheService(IRedisConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<T> GetAsync<T>(string key)
    {
        // Factory'den sor: Redis var mı?
        if (!_factory.IsRedisAvailable)
        {
            Console.WriteLine($"Redis yok - Cache miss: {key}");
            return default(T);
        }

        // Redis varsa kullan
        var connection = _factory.GetConnection();

        if (connection == null)
        {
            Console.WriteLine($"Redis connection null - Cache miss: {key}");
            return default(T);
        }

        try
        {
            var db = connection.GetDatabase();
            var json = await db.StringGetAsync(key);

            if (json.IsNullOrEmpty)
            {
                Console.WriteLine($"Redis'te veri yok: {key}");
                return default(T);
            }

            var result = JsonSerializer.Deserialize<T>(json);
            Console.WriteLine($"✅ Redis'ten alındı: {key}");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Redis okuma hatası: {ex.Message} - Key: {key}");

            // Bağlantı problemi varsa yeniden bağlanmayı dene
            _ = Task.Run(async () => await _factory.TryReconnectAsync());

            return default(T);
        }
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        // Redis yoksa hiçbir şey yapma
        if (!_factory.IsRedisAvailable)
        {
            Console.WriteLine($"Redis yok - Set atlandı: {key}");
            return;
        }

        var connection = _factory.GetConnection();

        if (connection == null)
        {
            Console.WriteLine($"Redis connection null - Set atlandı: {key}");
            return;
        }

        try
        {
            var db = connection.GetDatabase();
            var json = JsonSerializer.Serialize(value);
            await db.StringSetAsync(key, json, expiry);
            Console.WriteLine($"✅ Redis'e kaydedildi: {key}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Redis yazma hatası: {ex.Message} - Key: {key}");

            // Bağlantı problemi varsa yeniden bağlanmayı dene
            _ = Task.Run(async () => await _factory.TryReconnectAsync());
        }
    }

    public async Task RemoveAsync(string key)
    {
        if (!_factory.IsRedisAvailable) return;

        var connection = _factory.GetConnection();
        if (connection == null) return;

        try
        {
            var db = connection.GetDatabase();
            await db.KeyDeleteAsync(key);
            Console.WriteLine($"✅ Redis'ten silindi: {key}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Redis silme hatası: {ex.Message} - Key: {key}");
            _ = Task.Run(async () => await _factory.TryReconnectAsync());
        }
    }

    public async Task<bool> ExistsAsync(string key)
    {
        if (!_factory.IsRedisAvailable) return false;

        var connection = _factory.GetConnection();
        if (connection == null) return false;

        try
        {
            var db = connection.GetDatabase();
            var exists = await db.KeyExistsAsync(key);
            Console.WriteLine($"Redis key kontrolü: {key} = {exists}");
            return exists;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Redis kontrol hatası: {ex.Message} - Key: {key}");
            _ = Task.Run(async () => await _factory.TryReconnectAsync());
            return false;
        }
    }

    public async Task ClearAsync()
    {
        if (!_factory.IsRedisAvailable) return;

        var connection = _factory.GetConnection();
        if (connection == null) return;

        try
        {
            var endpoints = connection.GetEndPoints();
            foreach (var endpoint in endpoints)
            {
                var server = connection.GetServer(endpoint);
                await server.FlushDatabaseAsync();
            }
            Console.WriteLine("✅ Redis tamamen temizlendi");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Redis temizleme hatası: {ex.Message}");
            _ = Task.Run(async () => await _factory.TryReconnectAsync());
        }
    }
}
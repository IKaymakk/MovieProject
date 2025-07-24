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
        var connection = _factory.GetConnection();

        if (connection?.IsConnected != true)
        {
            Console.WriteLine($"Redis mevcut değil - Cache miss: {key}");
            return default;
        }

        try
        {
            var db = connection.GetDatabase();
            var json = await db.StringGetAsync(key);

            if (json.IsNullOrEmpty)
            {
                Console.WriteLine($"Redis'te veri yok: {key}");
                return default;
            }

            var result = JsonSerializer.Deserialize<T>(json);
            Console.WriteLine($"✅ Redis'ten alındı: {key}");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Redis okuma hatası: {ex.Message} - Key: {key}");
            return default;
        }
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var connection = _factory.GetConnection();

        if (connection?.IsConnected != true)
        {
            Console.WriteLine($"Redis mevcut değil - Set atlandı: {key}");
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
        }
    }

    public async Task RemoveAsync(string key)
    {
        var connection = _factory.GetConnection();

        if (connection?.IsConnected != true)
        {
            Console.WriteLine($"Redis mevcut değil - Delete atlandı: {key}");
            return;
        }

        try
        {
            var db = connection.GetDatabase();
            await db.KeyDeleteAsync(key);
            Console.WriteLine($"✅ Redis'ten silindi: {key}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Redis silme hatası: {ex.Message} - Key: {key}");
        }
    }

    public async Task<bool> ExistsAsync(string key)
    {
        var connection = _factory.GetConnection();

        if (connection?.IsConnected != true)
        {
            Console.WriteLine($"Redis mevcut değil - Exists false: {key}");
            return false;
        }

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
            return false;
        }
    }

    public async Task ClearAsync()
    {
        var connection = _factory.GetConnection();

        if (connection?.IsConnected != true)
        {
            Console.WriteLine("Redis mevcut değil - Clear atlandı");
            return;
        }

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
        }
    }
}
using MovieProject.Application.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieProject.Persistance.Repositories;

public class RedisCacheService : IRedisCacheService
{
    private readonly IDatabase _db;
    private readonly ConnectionMultiplexer _connection;

    public RedisCacheService(ConnectionMultiplexer redis)
    {
        _connection = redis;
        if (redis != null)
        {
            _db = _connection.GetDatabase();
        }
    }

    public async Task<T> GetAsync<T>(string key)
    {
        if (_connection == null) return default;

        var json = await _db.StringGetAsync(key);
        return json.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(json);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        if (_connection == null) return;

        var json = JsonSerializer.Serialize(value);
        await _db.StringSetAsync(key, json, expiry);
    }

    public async Task RemoveAsync(string key)
    {
        if (_connection == null) return;
        await _db.KeyDeleteAsync(key);
    }

    public async Task<bool> ExistsAsync(string key)
    {

        if (_connection == null) return false;
        return await _db.KeyExistsAsync(key);
    }

    public async Task ClearAsync()
    {
        if (_connection == null) return;

        var endpoints = _connection.GetEndPoints();
        foreach (var endpoint in endpoints)
        {
            var server = _connection.GetServer(endpoint);
            await server.FlushDatabaseAsync();
        }
    }

}
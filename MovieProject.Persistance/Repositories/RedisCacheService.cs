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
        _db = redis.GetDatabase();
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var json = await _db.StringGetAsync(key);
        return json.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(json);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var json = JsonSerializer.Serialize(value);
        await _db.StringSetAsync(key, json, expiry);
    }

    public async Task RemoveAsync(string key)
    {
        await _db.KeyDeleteAsync(key);
    }

    public async Task<bool> ExistsAsync(string key)
    {
        return await _db.KeyExistsAsync(key);
    }

    public async Task ClearAsync()
    {
        var endpoints = _connection.GetEndPoints();
        foreach (var endpoint in endpoints)
        {
            var server = _connection.GetServer(endpoint);
            await server.FlushDatabaseAsync();
        }
    }

}
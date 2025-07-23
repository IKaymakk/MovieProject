using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace MovieProject.Application.Interfaces.Redis;
public interface IRedisConnectionFactory
{
    IConnectionMultiplexer GetConnection();
    bool IsRedisAvailable { get; }
    Task<bool> TryReconnectAsync();
}

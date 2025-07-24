using MediatR;
using Microsoft.Extensions.Configuration;
using MovieProject.Application.Interfaces.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Behaviors;

public class RedisCacheBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IRedisCacheService _service;

    public RedisCacheBehavior(IRedisCacheService service)
    {
        _service = service;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not ICacheableQuery query)
            return await next();

        var cacheTime = query.CacheTime ?? TimeSpan.FromMinutes(10);
        var cacheKey = query.CacheKey;

        var cachedData = await _service.GetAsync<TResponse>(cacheKey);
        if (cachedData is not null)
            return cachedData;

        var response = await next();

        if (response is not null)
            await _service.SetAsync(cacheKey, response, cacheTime);

        return response;
    }
}
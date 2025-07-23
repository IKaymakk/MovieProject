using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using MovieProject.Application.Features.MovieGenre.Queries;
using MovieProject.Application.Features.MovieGenre.Results;
using MovieProject.Application.Interfaces;
using MovieProject.Application.Interfaces.Redis;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.MovieGenre.Handlers
{
    public class GetAllMoviesWithGenresQueryHandler : IRequestHandler<GetAllMoviesWithGenresQuery, List<GetAllMoviesWithGenresQueryResult>>
    {
        private readonly IMovieGenreRepository _movieGenreRepository;
        private readonly IMapper _mapper;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IConfiguration _configuration;

        public GetAllMoviesWithGenresQueryHandler(IMovieGenreRepository movieGenreRepository, IMapper mapper, IRedisCacheService redisCacheService, IConfiguration configuration)
        {
            _movieGenreRepository = movieGenreRepository;
            _mapper = mapper;
            _redisCacheService = redisCacheService;
            _configuration = configuration;
        }

        public async Task<List<GetAllMoviesWithGenresQueryResult>> Handle(GetAllMoviesWithGenresQuery request, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            const string cacheKey = "slider_movies_cache";
            var duration = TimeSpan.FromMinutes(_configuration.GetValue<int>("CacheSettings:DefaultCacheDuration", 5));

            // Cache'den çekmeyi dene
            try
            {
                var cachedData = await _redisCacheService.GetAsync<List<GetAllMoviesWithGenresQueryResult>>(cacheKey);
                if (cachedData != null)
                {
                    Console.WriteLine($"CACHE VERİSİ | Süre: {stopwatch.ElapsedMilliseconds} ms");
                    return cachedData;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Redis hatası (Get): {ex.Message} | Cache atlanıyor...");
            }

            // Cache boşsa DB'den çek
            var movieGenres = await _movieGenreRepository.GetAllMoviesWithGenres();

            var result = movieGenres.Select(movie => new GetAllMoviesWithGenresQueryResult
            {
                Id = movie.Id,
                Name = movie.Name,
                Image = movie.Image,
                Score = movie.Score,
                RunTime = movie.RunTime,
                ReleaseDate = movie.ReleaseDate,
                Genres = movie.Genres
            }).ToList();

            // Cache'e yazmayı dene
            try
            {
                await _redisCacheService.SetAsync(cacheKey, result, duration);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Redis hatası (Set): {ex.Message} | Cache yazılamadı.");
            }

            stopwatch.Stop();
            Console.WriteLine($"CACHE YOK | DB'den çekildi | Süre: {stopwatch.ElapsedMilliseconds} ms");
            return result;
        }

    }
}

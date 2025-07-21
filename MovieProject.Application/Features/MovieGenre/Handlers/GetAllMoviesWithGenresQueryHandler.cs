using AutoMapper;
using MediatR;
using MovieProject.Application.Features.MovieGenre.Queries;
using MovieProject.Application.Features.MovieGenre.Results;
using MovieProject.Application.Interfaces;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
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

        public GetAllMoviesWithGenresQueryHandler(IMovieGenreRepository movieGenreRepository, IMapper mapper, IRedisCacheService redisCacheService)
        {
            _movieGenreRepository = movieGenreRepository;
            _mapper = mapper;
            _redisCacheService = redisCacheService;
        }

        public async Task<List<GetAllMoviesWithGenresQueryResult>> Handle(GetAllMoviesWithGenresQuery request, CancellationToken cancellationToken)
        {
            const string cacheKey = "slider_movies_cache";

            // Cache’den dene
            var cachedData = await _redisCacheService.GetAsync<List<GetAllMoviesWithGenresQueryResult>>(cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }

            // Cache boşsa veritabanından çek
            var movieGenres = await _movieGenreRepository.GetAllMoviesWithGenres();

            var result = movieGenres.Select(movie => new GetAllMoviesWithGenresQueryResult
            {
                Id = movie.Id,
                Name = movie.Name,
                Image = movie.Image,
                Image2 = movie.Image2,
                Description = movie.Description,
                Description2 = movie.Description2,
                Director = movie.Director,
                Writer = movie.Writer,
                Trailer = movie.Trailer,
                HashTag = movie.HashTag,
                Score = movie.Score,
                CreatedDate = movie.CreatedDate,
                RunTime = movie.RunTime,
                ImbdScore = movie.ImbdScore,
                ReleaseDate = movie.ReleaseDate,

                Genres = movie.MovieGenres.Select(g => g.Genre.Name).ToList()

            }).ToList();

            // Sonucu cache’e koy, örneğin 5 dakika sakla
            await _redisCacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }

    }
}

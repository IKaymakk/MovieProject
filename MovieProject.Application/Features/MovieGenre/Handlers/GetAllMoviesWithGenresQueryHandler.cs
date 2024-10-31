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

        public GetAllMoviesWithGenresQueryHandler(IMovieGenreRepository movieGenreRepository, IMapper mapper)
        {
            _movieGenreRepository = movieGenreRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllMoviesWithGenresQueryResult>> Handle(GetAllMoviesWithGenresQuery request, CancellationToken cancellationToken)
        {
            var movieGenres = await _movieGenreRepository.GetAllMoviesWithGenres();


            //AutoMapper Kullansaydık
            //var mappedmovieswithgenres = _mapper.Map<List<GetAllMoviesWithGenresQueryResult>>(movieGenres);
            //return mappedmovieswithgenres;
            //AutoMapper Kullansaydık


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
                Year = movie.Year,
                RunTime = movie.RunTime,
                ImbdScore = movie.ImbdScore,
                ReleaseDate = movie.ReleaseDate,

                // Genre bilgilerini mapliyoruz
                Genres = movie.MovieGenres.Select(g => g.Genre.Name).ToList() // Sadece isimleri alıyoruz

            }).ToList();

            return result;

        }
    }
}

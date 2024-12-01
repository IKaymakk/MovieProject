using MediatR;
using MovieProject.Application.Features.Movie.Queries;
using MovieProject.Application.Features.Movie.Results;
using MovieProject.Application.Features.MovieGenre.Results;
using MovieProject.Application.Interfaces;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Handler
{
    public class GetMovieDetailsQueryHandler : IRequestHandler<GetMovieDetailsQuery, GetMovieDetailsQueryResult>
    {
        private readonly IMovieRepository _repository;

        public GetMovieDetailsQueryHandler(IMovieRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetMovieDetailsQueryResult> Handle(GetMovieDetailsQuery request, CancellationToken cancellationToken)
        {
            var film = await _repository.GetMovieDetails(request.id);

            var result = new GetMovieDetailsQueryResult
            {
                Id = film.Id,
                Name = film.Name,
                Image = film.Image,
                Image2 = film.Image2,
                Description = film.Description,
                Description2 = film.Description2,
                Director = film.Director,
                Writer = film.Writer,
                Trailer = film.Trailer,
                HashTag = film.HashTag,
                Score = film.Score,
                ImbdScore = film.ImbdScore,
                RunTime = film.RunTime,
                CreatedDate = film.CreatedDate,
                ReleaseDate = film.ReleaseDate,

                Genres = film.MovieGenres.Select(mg => mg.Genre.Name).ToList(), // Türleri string olarak mapliyoruz
            };

            return result;
        }

    }
}

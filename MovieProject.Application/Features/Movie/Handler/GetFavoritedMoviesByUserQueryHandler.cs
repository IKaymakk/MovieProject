using AutoMapper;
using MediatR;
using MovieProject.Application.Features.Movie.Queries;
using MovieProject.Application.Features.Movie.Results;
using MovieProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Handler
{
    public class GetFavoritedMoviesByUserQueryHandler : IRequestHandler<GetFavoritedMoviesByUserQuery, List<GetFavoritedMoviesByUserQueryResult>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public GetFavoritedMoviesByUserQueryHandler(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<List<GetFavoritedMoviesByUserQueryResult>> Handle(GetFavoritedMoviesByUserQuery request, CancellationToken cancellationToken)
        {
            var favoritedMovies = await _movieRepository.GetFavoritedMoviesByUser(request.userid);
            var mappedFavoritedMovies = _mapper.Map<List<GetFavoritedMoviesByUserQueryResult>>(favoritedMovies);
            return mappedFavoritedMovies;
        }
    }
}

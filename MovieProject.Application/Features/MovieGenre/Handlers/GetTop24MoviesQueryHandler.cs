using AutoMapper;
using MediatR;
using MovieProject.Application.Features.MovieGenre.Queries;
using MovieProject.Application.Features.MovieGenre.Results;
using MovieProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.MovieGenre.Handlers
{
    public class GetTop24MoviesQueryHandler : IRequestHandler<GetTop24MoviesQuery, List<GetTop24MoviesQueryResult>>
    {
        private readonly IMovieGenreRepository _movieGenreRepository;
        private readonly IMapper _mapper;

        public GetTop24MoviesQueryHandler(IMovieGenreRepository movieGenreRepository, IMapper mapper)
        {
            _movieGenreRepository = movieGenreRepository;
            _mapper = mapper;
        }

        public async Task<List<GetTop24MoviesQueryResult>> Handle(GetTop24MoviesQuery request, CancellationToken cancellationToken)
        {
            var top24movies = await _movieGenreRepository.GetTop24Movies();
            var mappedtop24movies = _mapper.Map<List<GetTop24MoviesQueryResult>>(top24movies);
            return mappedtop24movies;
        }
    }
}

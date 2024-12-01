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
    public class GetLast15MoviesQueryHandler : IRequestHandler<GetLast15MoviesQuery, List<GetLast15MoviesQueryResult>>
    {
        private readonly IMovieRepository _repository;
        private readonly IMapper _mapper;

        public GetLast15MoviesQueryHandler(IMovieRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetLast15MoviesQueryResult>> Handle(GetLast15MoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = await _repository.GetLast24Movie();
            var mappedmovies = _mapper.Map<List<GetLast15MoviesQueryResult>>(movies);
            return mappedmovies;
        }
    }
}

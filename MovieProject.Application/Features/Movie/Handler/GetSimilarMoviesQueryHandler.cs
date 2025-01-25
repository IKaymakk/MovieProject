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
    public class GetSimilarMoviesQueryHandler : IRequestHandler<GetSimilarMoviesQuery, List<GetSimilarMoviesQueryResult>>
    {
        private readonly IMovieRepository _repository;
        private readonly IMapper _mapper;

        public GetSimilarMoviesQueryHandler(IMovieRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetSimilarMoviesQueryResult>> Handle(GetSimilarMoviesQuery request, CancellationToken cancellationToken)
        {
            var similarMovies = await _repository.GetSimilarMovies(request.Hashtag);
            var mappedSimilarMovies = _mapper.Map<List<GetSimilarMoviesQueryResult>>(similarMovies);
            return mappedSimilarMovies;
        }
    }
}

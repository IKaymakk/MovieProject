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
    public class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, List<GetAllMoviesQueryResult>>
    {
        private readonly IRepository<MovieProject_Domain.Entities.Movie> _repository;
        private readonly IMapper _mapper;

        public GetAllMoviesQueryHandler(IRepository<MovieProject_Domain.Entities.Movie> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllMoviesQueryResult>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = await _repository.GetAllAsync();
            var mappedmovies = _mapper.Map<List<GetAllMoviesQueryResult>>(movies);
            return mappedmovies;
        }
    }
}

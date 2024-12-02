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
    public class GetMoviesByGenreQueryHandler : IRequestHandler<GetMoviesByGenreQuery, List<GetMoviesByGenreQueryResult>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public GetMoviesByGenreQueryHandler(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<List<GetMoviesByGenreQueryResult>> Handle(GetMoviesByGenreQuery request, CancellationToken cancellationToken)
        {
            var values = await _movieRepository.GetMoviesByCategory(request.id);
            var mappedvalues = _mapper.Map<List<GetMoviesByGenreQueryResult>>(values);
            return mappedvalues;
        }
    }
}

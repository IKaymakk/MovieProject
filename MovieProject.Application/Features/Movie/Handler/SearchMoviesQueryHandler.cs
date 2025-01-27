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
    public class SearchMoviesQueryHandler : IRequestHandler<SearchMoviesQuery, PaginatedMovieResult>
    {
        private readonly IMovieRepository _repository;
        private readonly IMapper _mapper;

        public SearchMoviesQueryHandler(IMovieRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedMovieResult> Handle(SearchMoviesQuery request, CancellationToken cancellationToken)
        {
            var (movies, totalCount) = await _repository.SearchMoviesWithSortingAndCount(request.SearchTerm, request.SortBy, request.Page, request.PageSize);
          
            var mappedMovies = _mapper.Map<List<GetMoviesByFilterQueryResult>>(movies);

            var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            return new PaginatedMovieResult
            {
                Movies = mappedMovies,
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = request.Page
            };


        }
    }
}

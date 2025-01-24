using AutoMapper;
using MediatR;
using MovieProject.Application.DTOS;
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
    public class GetMoviesByGenreQueryHandler : IRequestHandler<GetMoviesByGenreQuery, PaginatedMovieResult>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public GetMoviesByGenreQueryHandler(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedMovieResult> Handle(GetMoviesByGenreQuery request, CancellationToken cancellationToken)
        {
            var options = new FilterListDto
            {
                SortBy = request.SortBy,
                Page = request.Page,
                PageSize = request.PageSize
            };

            var (values, totalcount) = await _movieRepository.GetMoviesByCategoryWithPaging(options, request.id);

            var mappedvalues = _mapper.Map<List<GetMoviesByFilterQueryResult>>(values);

            var totalPages = (int)Math.Ceiling(totalcount / (double)request.PageSize);

            return new PaginatedMovieResult
            {
                id = request.id,
                Movies = mappedvalues,
                TotalCount = totalcount,
                TotalPages = totalPages,
                CurrentPage = request.Page
            };
        }
    }
}

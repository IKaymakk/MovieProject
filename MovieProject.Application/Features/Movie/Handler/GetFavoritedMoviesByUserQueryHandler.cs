using AutoMapper;
using MediatR;
using MovieProject.Application.Features.Movie.Queries;
using MovieProject.Application.Features.Movie.Results;
using MovieProject.Application.Interfaces;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Handler
{
    public class GetFavoritedMoviesByUserQueryHandler : IRequestHandler<GetFavoritedMoviesByUserQuery, PaginatedMovieResult>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public GetFavoritedMoviesByUserQueryHandler(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedMovieResult> Handle(GetFavoritedMoviesByUserQuery request, CancellationToken cancellationToken)
        {
            var (movies, totalCount) = await _movieRepository.GetFavoritedMoviesByUser(request.userid, request.SortBy, request.Page, request.PageSize);
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

        //public async Task<List<GetFavoritedMoviesByUserQueryResult>> Handle(GetFavoritedMoviesByUserQuery request, CancellationToken cancellationToken)
        //{
        //    var favoritedMovies = await _movieRepository.GetFavoritedMoviesByUser(request.userid);
        //    var mappedFavoritedMovies = _mapper.Map<List<GetFavoritedMoviesByUserQueryResult>>(favoritedMovies);
        //    return mappedFavoritedMovies;
        //}
    }
}

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

namespace MovieProject.Application.Features.Movie.Handler;

public class GetMoviesByFilterQueryHandler : IRequestHandler<GetMoviesByFilterQuery, PaginatedMovieResult>
{
    private readonly IMovieRepository _repository;
    private readonly IMapper _mapper;

    public GetMoviesByFilterQueryHandler(IMovieRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedMovieResult> Handle(GetMoviesByFilterQuery request, CancellationToken cancellationToken)
    {
        var options = new FilterListDto
        {
            SortBy = request.SortBy,
            Page = request.Page,
            PageSize = request.PageSize
        };

        // Repo'dan veri ve toplam kayıt sayısını al
        var (movies, totalCount) = await _repository.GetFilterMoviesListWithCount(options);
        var mappedMovies = _mapper.Map<List<GetMoviesByFilterQueryResult>>(movies);

        // Toplam sayfa sayısını hesapla
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

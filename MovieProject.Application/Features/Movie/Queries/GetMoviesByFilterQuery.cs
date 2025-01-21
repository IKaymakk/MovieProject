using MediatR;
using MovieProject.Application.Features.Movie.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Queries
{
    public class GetMoviesByFilterQuery : IRequest<PaginatedMovieResult>
    {
        public string? SortBy { get; set; } // Sıralama kriteri
        public int Page { get; set; } = 1;  // Varsayılan sayfa
        public int PageSize { get; set; } = 10; // Varsayılan sayfa boyutu

        public GetMoviesByFilterQuery()
        {

        }
        public GetMoviesByFilterQuery(string? sortBy, int page, int pageSize)
        {
            SortBy = sortBy;
            Page = page;
            PageSize = pageSize;
        }
    }
}

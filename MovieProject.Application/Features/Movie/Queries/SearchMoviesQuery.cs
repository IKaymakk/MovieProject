using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Queries
{
    public class SearchMoviesQuery : IRequest<PaginatedMovieResult>
    {
        public string? SearchTerm { get; set; }
        public string SortBy { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public SearchMoviesQuery(string? searchTerm, string? sortBy, int page, int pageSize)
        {
            SearchTerm = searchTerm;
            SortBy = sortBy;
            Page = page;
            PageSize = pageSize;
        }
        public SearchMoviesQuery()
        {

        }
    }
}

using MediatR;
using MovieProject.Application.Features.Movie.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Queries
{
    public class GetMoviesByGenreQuery : IRequest<PaginatedMovieResult>
    {
        public int id { get; set; }
        public string? SortBy { get; set; } // Sıralama kriteri
        public int Page { get; set; } = 1;  // Varsayılan sayfa
        public int PageSize { get; set; } = 10; // Varsayılan sayfa boyutu

        public GetMoviesByGenreQuery()
        {

        }
        public GetMoviesByGenreQuery(int id, int pageSize, int page, string sortBy)
        {
            this.id = id;
            PageSize = pageSize;
            Page = page;
            SortBy = sortBy;
        }
    }
}

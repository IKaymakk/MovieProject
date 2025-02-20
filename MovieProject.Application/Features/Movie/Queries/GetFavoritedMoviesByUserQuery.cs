using MediatR;
using MovieProject.Application.Features.Movie.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Queries
{
    public class GetFavoritedMoviesByUserQuery : IRequest<PaginatedMovieResult>
    {
        public int userid { get; set; }
        public string SortBy { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public GetFavoritedMoviesByUserQuery(int userid,  string sortBy, int page, int pageSize)
        {
            this.userid = userid;
            SortBy = sortBy;
            Page = page;
            PageSize = pageSize;
        }
        public GetFavoritedMoviesByUserQuery()
        {

        }
    }
}

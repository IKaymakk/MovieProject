using MediatR;
using MovieProject.Application.Features.Comment.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Comment.Queries
{
    public class GetCommentsByMovieQuery : IRequest<PaginatedCommentResult>
    {
        public int id { get; set; }
        public string? SortBy { get; set; } // Sıralama kriteri
        public int Page { get; set; } = 1;  // Varsayılan sayfa
        public int PageSize { get; set; } = 5; // Varsayılan sayfa boyutu

       
        public GetCommentsByMovieQuery(int id, string? sortBy, int page, int pageSize)
        {
            this.id = id;
            SortBy = sortBy;
            Page = page;
            PageSize = pageSize;
        }

        public GetCommentsByMovieQuery()
        {

        }

    }
}

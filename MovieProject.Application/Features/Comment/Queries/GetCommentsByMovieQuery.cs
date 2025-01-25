using MediatR;
using MovieProject.Application.Features.Comment.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Comment.Queries
{
    public class GetCommentsByMovieQuery : IRequest<List<GetCommentsByMovieQueryResult>>
    {
        public int id { get; set; }

        public GetCommentsByMovieQuery(int id)
        {
            this.id = id;
        }
    }
}

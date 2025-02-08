using MediatR;
using MovieProject.Application.Features.Comment.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Comment.Queries
{
        
    public class GetCommentsByUserIdQuery:IRequest<List<GetCommentsByUserIdQueryResult>>
    {
        public int userId { get; set; }

        public GetCommentsByUserIdQuery(int userId)
        {
            this.userId = userId;
        }
    }
}

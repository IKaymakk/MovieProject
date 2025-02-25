using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Comment.Commands
{
    public class AddCommentCommand : IRequest
    {
        public string CommentTitle { get; set; }
        public string CommentText { get; set; }
        public int MovieId { get; set; }
        public int AppUserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

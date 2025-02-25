using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Comment.Results
{
    public class GetCommentsByMovieQueryResult
    {
        public int AppUserId { get; set; }
        public int MovieId { get; set; }
        public string CommentTitle { get; set; }
        public string CommentText { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AppUserImage { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

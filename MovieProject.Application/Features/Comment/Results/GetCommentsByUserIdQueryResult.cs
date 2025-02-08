using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Comment.Results
{
    public class GetCommentsByUserIdQueryResult
    {
        public int AppUserId { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; }

        public string MovieImage { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime MovieReleaseDate { get; set; }

    }
}

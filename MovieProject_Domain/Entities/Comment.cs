using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject_Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual AppUser AppUser { get; set; }
        public int MovieId { get; set; }
        public int AppUserId { get; set; }
    }
}

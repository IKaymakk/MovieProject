using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject_Domain.Entities
{
    public class FavoriteMovie
    {
        public int AppUserId { get; set; } // Kullanıcı ID'si
        public AppUser AppUser { get; set; } // Kullanıcı

        public int MovieId { get; set; } // Film ID'si
        public Movie Movie { get; set; } // Film

    }
}

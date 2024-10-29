﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject_Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Image2 { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Trailer { get; set; }
        public string HashTag { get; set; }

        public int Score { get; set; }
        public int Year { get; set; }
        public int RunTime { get; set; }
        public int ImbdScore { get; set; }

        public DateTime ReleaseDate { get; set; }

        public ICollection<MovieGenre> MovieGenres { get; set; } // Ara tabloya referans
        public ICollection<FavoriteMovie> FavoriteMovies { get; set; } // Ara tabloya referans

    }
}
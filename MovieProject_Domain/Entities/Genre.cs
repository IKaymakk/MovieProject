﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject_Domain.Entities;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Image { get; set; }
    public ICollection<MovieGenre> MovieGenres { get; set; } // Ara tabloya referans
}
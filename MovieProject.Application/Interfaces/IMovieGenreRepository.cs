using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieProject.Application.DTOS;

namespace MovieProject.Application.Interfaces;

public interface IMovieGenreRepository : IRepository<MovieGenre>
{
    Task<List<MovieDto>> GetAllMoviesWithGenres();

    Task<List<Movie>> GetTop24Movies();
}
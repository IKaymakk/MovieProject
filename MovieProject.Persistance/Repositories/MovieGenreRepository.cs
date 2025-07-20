using Microsoft.EntityFrameworkCore;
using MovieProject.Application.Interfaces;
using MovieProject.Persistance.Context;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Persistance.Repositories
{
    public class MovieGenreRepository : IMovieGenreRepository
    {
        public MovieContext _context;

        public MovieGenreRepository(MovieContext context)
        {
            _context = context;
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(MovieGenre entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Movie>> GetAllMoviesWithGenres()
        {
            var movies = await _context.Movies
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                    .OrderBy(x => x.CreatedDate)
                    .Take(5)
                .ToListAsync();

            return movies;
        }

        public Task<List<MovieGenre>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<MovieGenre>> GetAllWithIncludesAsync(Expression<Func<MovieGenre, bool>> filter = null, params Expression<Func<MovieGenre, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<MovieGenre, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<MovieGenre> GetByFilterAsync(Expression<Func<MovieGenre, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<MovieGenre> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(MovieGenre entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(MovieGenre entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Movie>> GetTop24Movies()
        {
            var top24movies = await _context.Movies
                .AsNoTracking()
                .Include(x => x.MovieGenres)
                    .ThenInclude(x => x.Genre)
                .OrderByDescending(x => x.Score)
                .Take(24)
                .ToListAsync();
            return top24movies;
        }

      


    }
}

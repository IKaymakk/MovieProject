using Microsoft.EntityFrameworkCore;
using MovieProject.Application.Features.Movie.Queries;
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
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;

        public MovieRepository(MovieContext context)
        {
            _context = context;
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Movie>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Movie>> GetAllWithIncludesAsync(Expression<Func<Movie, bool>> filter = null, params Expression<Func<Movie, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetByFilterAsync(Expression<Func<Movie, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Movie>> GetLast24Movie()
        {
            var movies = await _context.Movies
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Take(24)
                .ToListAsync();
            return movies;
        }

        public async Task<Movie> GetMovieDetails(int id)
        {
            var movie = await _context.Movies
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.MovieGenres)
                .ThenInclude(x => x.Genre)
                .FirstOrDefaultAsync();
            return movie;
        }

        public Task RemoveAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Movie entity)
        {
            throw new NotImplementedException();
        }
    }
}

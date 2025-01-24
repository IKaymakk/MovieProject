using Microsoft.EntityFrameworkCore;
using MovieProject.Application.DTOS;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<List<Movie>> GetFilterMoviesList(FilterListDto options)
        {
            var query = _context.Movies
                .AsNoTracking()
                .Include(x => x.MovieGenres)
                .ThenInclude(x => x.Genre)
                .OrderByDescending(x => x.Id)
                  .Select(x => new Movie
                  {
                      Id = x.Id,
                      Name = x.Name,
                      Image = x.Image,
                      Score = x.Score,
                      ReleaseDate = x.ReleaseDate
                  });

            // Sıralama
            if (!string.IsNullOrEmpty(options.SortBy))
            {
                query = options.SortBy switch
                {
                    "NameAsc" => query.OrderBy(x => x.Name),
                    "NameDesc" => query.OrderByDescending(x => x.Name),
                    "ScoreAsc" => query.OrderBy(x => x.Score),
                    "ScoreDesc" => query.OrderByDescending(x => x.Score),
                    "ReleaseDateAsc" => query.OrderBy(x => x.ReleaseDate),
                    "ReleaseDateDesc" => query.OrderByDescending(x => x.ReleaseDate),
                    _ => query // Default sıralama yok
                };
            }
            return await query.ToListAsync();
        }

        public async Task<(List<Movie>, int)> GetFilterMoviesListWithCount(FilterListDto options)
        {
            var query = _context.Movies
                .AsNoTracking()
                .Include(x => x.MovieGenres)
                .ThenInclude(x => x.Genre)
                .Select(x => new Movie
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Score = x.Score,
                    ReleaseDate = x.ReleaseDate
                });

            // Sıralama
            if (!string.IsNullOrEmpty(options.SortBy))
            {
                query = options.SortBy switch
                {
                    "NameAsc" => query.OrderBy(x => x.Name),
                    "NameDesc" => query.OrderByDescending(x => x.Name),
                    "ScoreAsc" => query.OrderBy(x => x.Score),
                    "ScoreDesc" => query.OrderByDescending(x => x.Score),
                    "ReleaseDateAsc" => query.OrderBy(x => x.ReleaseDate),
                    "ReleaseDateDesc" => query.OrderByDescending(x => x.ReleaseDate),
                    _ => query
                };
            }

            // Toplam kayıt sayısı
            var totalCount = await query.CountAsync();

            // Sayfalama
            if (options.Page > 0 && options.PageSize > 0)
            {
                var skip = (options.Page - 1) * options.PageSize;
                query = query.Skip(skip).Take(options.PageSize);
            }

            var movies = await query.ToListAsync();
            return (movies, totalCount);
        }

        public async Task<(List<Movie>, int)> GetMoviesByCategoryWithPaging(FilterListDto options, int id)
        {
            // İlk sorgu
            var query = _context.Movies
                            .AsNoTracking()
                            .Where(x => x.MovieGenres.Any(mg => mg.GenreId == id))
                            .Select(x => new Movie
                            {
                                Id = x.Id,
                                Name = x.Name,
                                Image = x.Image,
                                Score = x.Score,
                                ReleaseDate = x.ReleaseDate
                            });

            // Sıralama işlemi
            if (!string.IsNullOrEmpty(options.SortBy))
            {
                query = options.SortBy switch
                {
                    "NameAsc" => query.OrderBy(x => x.Name),
                    "NameDesc" => query.OrderByDescending(x => x.Name),
                    "ScoreAsc" => query.OrderBy(x => x.Score),
                    "ScoreDesc" => query.OrderByDescending(x => x.Score),
                    "ReleaseDateAsc" => query.OrderBy(x => x.ReleaseDate),
                    "ReleaseDateDesc" => query.OrderByDescending(x => x.ReleaseDate),
                    _ => query
                };
            }

            // Geri dönüş
            var totalCount = await query.CountAsync(); // Sorgu burada çalıştırılır.

            // Sayfalama
            if (options.Page > 0 && options.PageSize > 0)
            {
                var skip = (options.Page - 1) * options.PageSize;
                query = query.Skip(skip).Take(options.PageSize); // Skip ve Take uygulanır.
            }

            var movies = await query.ToListAsync(); // Sorgu burada tamamlanır.
            return (movies, totalCount);

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


        public async Task<List<Movie>> GetMoviesByCategory(int id)
        {
            var movies = await _context.Movies
                .AsNoTracking()
                .Where(x => x.MovieGenres.Any(mg => mg.GenreId == id))
                .Select(x => new Movie
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Score = x.Score
                })
                .ToListAsync();

            return movies;
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


//// Filtreleme
//if (options.GenreId != null)
//{
//    query = query.Where(x => x.MovieGenres.Any(mg => mg.GenreId == options.GenreId));
//}

//if (!string.IsNullOrEmpty(options.Name))
//{
//    query = query.Where(x => x.Name.Contains(options.Name));
//}

//if (options.Score != null)
//{
//    query = query.Where(x => x.Score == options.Score);
//}

//if (options.ReleaseDate != null)
//{
//    query = query.Where(x => x.ReleaseDate == options.ReleaseDate);
//}
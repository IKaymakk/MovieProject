using Microsoft.EntityFrameworkCore;
using MovieProject.Application.Interfaces;
using MovieProject.Persistance.Context;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Persistance.Repositories;

public class FavoriteMovieRepository : IFavoriteMovieRepository
{
    private readonly MovieContext _context;

    public FavoriteMovieRepository(MovieContext context)
    {
        _context = context;
    }

    public async Task<bool> ChangeFavoritedStatus(int userId, int movieId)
    {
        var isFavorited = await _context.FavoriteMovies
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.AppUserId == userId && x.MovieId == movieId);

        if (isFavorited == null)
        {
            _context.FavoriteMovies.Add(new FavoriteMovie
            {
                AppUserId = userId,
                MovieId = movieId
            });

            await _context.SaveChangesAsync(); 
            return true; 
        }
        else
        {
            var favmovie = await _context.FavoriteMovies
                .FirstOrDefaultAsync(x => x.AppUserId == userId && x.MovieId == movieId);

            if (favmovie != null)
            {
                _context.FavoriteMovies.Remove(favmovie);
                await _context.SaveChangesAsync();
            }

            return false;
        }
    }

}

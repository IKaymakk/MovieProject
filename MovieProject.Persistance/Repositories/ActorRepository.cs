using Microsoft.EntityFrameworkCore;
using MovieProject.Application.Interfaces;
using MovieProject.Persistance.Context;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Persistance.Repositories
{
    public class ActorRepository : Repository<Actor>, IActorRepository
    {
        private readonly MovieContext _context;

        public ActorRepository(MovieContext movieContext, MovieContext context) : base(movieContext)
        {
            _context = context;
        }

        public async Task<List<Actor>> GetMovieActors(int id)
        {
            var movieactors = await _context.Actors
                .Where(x => x.MovieId == id)
                .ToListAsync();
            return movieactors;
        }


    }
}

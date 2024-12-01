using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Interfaces
{
    public interface IActorRepository:IRepository<Actor>
    {
        Task<List<Actor>> GetMovieActors(int id);
    }
}

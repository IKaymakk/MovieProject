using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<List<Movie>> GetLast24Movie();
        Task<Movie> GetMovieDetails(int id);
        Task<List<Movie>> GetMoviesByCategory(int id);


    }
}
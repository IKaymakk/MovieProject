using MovieProject.Application.Interfaces;
using MovieProject.Persistance.Context;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Persistance.Repositories;

public class GenreRepository : Repository<Genre>, IGenreRepository
{
    public GenreRepository(MovieContext movieContext) : base(movieContext)
    {
    }
}
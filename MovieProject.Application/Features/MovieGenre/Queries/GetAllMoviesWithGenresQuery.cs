using MediatR;
using MovieProject.Application.Features.MovieGenre.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.MovieGenre.Queries
{
    public class GetAllMoviesWithGenresQuery : IRequest<List<GetAllMoviesWithGenresQueryResult>>
    {
    }
}

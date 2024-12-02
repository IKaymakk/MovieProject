using MediatR;
using MovieProject.Application.Features.Movie.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Queries
{
    public class GetMoviesByGenreQuery:IRequest<List<GetMoviesByGenreQueryResult>>
    {
        public int id { get; set; }

        public GetMoviesByGenreQuery(int id)
        {
            this.id = id;
        }
    }
}

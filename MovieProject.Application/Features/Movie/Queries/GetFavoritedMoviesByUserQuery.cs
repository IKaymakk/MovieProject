using MediatR;
using MovieProject.Application.Features.Movie.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Queries
{
    public class GetFavoritedMoviesByUserQuery : IRequest<List<GetFavoritedMoviesByUserQueryResult>>
    {
        public int userid { get; set; }

        public GetFavoritedMoviesByUserQuery(int userid)
        {
            this.userid = userid;
        }
    }
}

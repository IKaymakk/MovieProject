using MediatR;
using MovieProject.Application.Features.Movie.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Queries
{
    public class GetMovieByIdQuery : IRequest<GetMovieByIdQueryResult>
    {
        public int Id { get; set; }

        public GetMovieByIdQuery(int ıd)
        {
            Id = ıd;
        }
    }
}

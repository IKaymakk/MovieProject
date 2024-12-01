using MediatR;
using MovieProject.Application.Features.Actor.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Actor.Queries
{
    public class GetMovieActorsQuery:IRequest<List<GetMovieActorsQueryResult>>
    {
        public int Id { get; set; }

        public GetMovieActorsQuery(int ıd)
        {
            Id = ıd;
        }
    }
}

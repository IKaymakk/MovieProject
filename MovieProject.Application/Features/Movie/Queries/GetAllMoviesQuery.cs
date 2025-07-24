using MediatR;
using MovieProject.Application.Features.Movie.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieProject.Application.Interfaces.Redis;

namespace MovieProject.Application.Features.Movie.Queries
{
    public class GetAllMoviesQuery:IRequest<List<GetAllMoviesQueryResult>>, ICacheableQuery
    {
        public string CacheKey => "GetAllMovies";
        public TimeSpan? CacheTime => TimeSpan.FromMinutes(10);
    }
}

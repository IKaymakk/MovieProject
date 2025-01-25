using MediatR;
using MovieProject.Application.Features.Movie.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Queries
{
    public class GetSimilarMoviesQuery : IRequest<List<GetSimilarMoviesQueryResult>>
    {
        public string Hashtag { get; set; }

        public GetSimilarMoviesQuery(string hashtag)
        {
            Hashtag = hashtag;
        }
    }
}

﻿using MediatR;
using MovieProject.Application.Features.Movie.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Queries
{
    public class GetMovieDetailsQuery(int id) : IRequest<GetMovieDetailsQueryResult>
    {
        public int id { get; set; } = id;
    }
}

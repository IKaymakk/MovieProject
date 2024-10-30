﻿using AutoMapper;
using MediatR;
using MovieProject.Application.Features.Movie.Queries;
using MovieProject.Application.Features.Movie.Results;
using MovieProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.Movie.Handler
{
    public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery,GetMovieByIdQueryResult>
    {
        private readonly IRepository<MovieProject_Domain.Entities.Movie> _repository;
        private readonly IMapper _mapper;

        public GetMovieByIdQueryHandler(IRepository<MovieProject_Domain.Entities.Movie> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetMovieByIdQueryResult> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var movie = await _repository.GetByIdAsync(request.Id);
            var mappedmovie = _mapper.Map<GetMovieByIdQueryResult>(movie);
            return mappedmovie;
        }
    }
}
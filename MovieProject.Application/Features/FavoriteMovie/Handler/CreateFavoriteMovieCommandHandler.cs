using AutoMapper;
using MediatR;
using MovieProject.Application.Features.FavoriteMovie.Command;
using MovieProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.FavoriteMovie.Handler
{
    public class CreateFavoriteMovieCommandHandler : IRequestHandler<CreateFavoriteMovieCommand>
    {
        private readonly IRepository<MovieProject_Domain.Entities.FavoriteMovie> _repository;
        private readonly IMapper _mapper;

        public CreateFavoriteMovieCommandHandler(IRepository<MovieProject_Domain.Entities.FavoriteMovie> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreateFavoriteMovieCommand request, CancellationToken cancellationToken)
        {
            var mappedFavMovies = _mapper.Map<MovieProject_Domain.Entities.FavoriteMovie>(request);
            await _repository.CreateAsync(mappedFavMovies);
        }
    }
}

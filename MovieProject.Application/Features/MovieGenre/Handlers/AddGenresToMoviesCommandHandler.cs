using AutoMapper;
using MediatR;
using MovieProject.Application.Features.MovieGenre.Commands;
using MovieProject.Application.Interfaces;
using MovieProject_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Features.MovieGenre.Handlers
{
    public class AddGenresToMoviesCommandHandler : IRequestHandler<AddGenresToMoviesCommand>
    {
        private readonly IRepository<MovieProject_Domain.Entities.MovieGenre> _repository;
        private readonly IMapper _mapper;

        public AddGenresToMoviesCommandHandler(IRepository<MovieProject_Domain.Entities.MovieGenre> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(AddGenresToMoviesCommand request, CancellationToken cancellationToken)
        {
            var newmoviegenre = new MovieProject_Domain.Entities.MovieGenre()
            {
                MovieId = request.MovieId,
                GenreId = request.GenreId,
            };

            newmoviegenre.Id = 1;
            await _repository.CreateAsync(newmoviegenre);
        }
    }
}
